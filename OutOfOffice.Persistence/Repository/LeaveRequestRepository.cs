using Microsoft.EntityFrameworkCore;
using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Persistence.Repository
{
    public class LeaveRequestRepository : ILeaveRequestStore
    {
        private readonly OutOfOfficeDbContext? _context;

        public LeaveRequestRepository(OutOfOfficeDbContext context)
        {
            _context = context;
        }

        // Get all leave requests
        public async Task<IEnumerable<LeaveRequestEntity>> GetAsync()
        {
            return await _context.LeaveRequests.AsNoTracking().ToListAsync();
        }

        // Get the leave request by id
        public async Task<LeaveRequestEntity> GetByIdAsync(Guid id)
        {
            return await _context.LeaveRequests.AsNoTracking().FirstOrDefaultAsync(lr => lr.Id == id);
        }

        // Add the leave request
        public async Task AddAsync(LeaveRequestEntity leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
        }

        // Update the leave request
        public async Task UpdateAsync(LeaveRequestEntity leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task SubmitLeaveRequestAsync(Guid id)
        {
            LeaveRequestEntity leaveRequest = await _context.LeaveRequests.AsNoTracking().FirstOrDefaultAsync(lr => lr.Id == id);

            leaveRequest.Status = "Submitted";

            _context.LeaveRequests.Update(leaveRequest);

            if (leaveRequest.ApprovalRequests.Count > 0)
            {
                var appReq = leaveRequest.ApprovalRequests.FirstOrDefault();
                appReq.Status = "New";
                _context.ApprovalRequests.Update(appReq);
            }
            else 
            {
                var approvalRequest = new ApprovalRequestEntity
                {
                    LeaveRequestId = leaveRequest.Id
                };
                await _context.ApprovalRequests.AddAsync(approvalRequest);
            }

            await _context.SaveChangesAsync();
        }

        public async Task CancelLeaveRequestAsync(Guid id)
        {
            LeaveRequestEntity leaveRequest = await _context.LeaveRequests.AsNoTracking().FirstOrDefaultAsync(lr => lr.Id == id);

            leaveRequest.Status = "Canceled";

            _context.LeaveRequests.Update(leaveRequest);

            ApprovalRequestEntity approveRequst = await _context.ApprovalRequests.FirstOrDefaultAsync(x => x.LeaveRequestId == leaveRequest.Id /*&& x.Status != "Canceled"*/);

            if (approveRequst != null)
            {
                approveRequst.Status = "Canceled";
                _context.ApprovalRequests.Update(approveRequst);
            }

            await _context.SaveChangesAsync();
        }
    }
}
