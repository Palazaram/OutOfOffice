using Microsoft.EntityFrameworkCore;
using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Persistence.Repository
{
    public class ApprovalRequestRepository : IApprovalRequestStore
    {
        private readonly OutOfOfficeDbContext? _context;

        public ApprovalRequestRepository(OutOfOfficeDbContext context)
        {
            _context = context;
        }

        // Get all approval requests
        public async Task<IEnumerable<ApprovalRequestEntity>> GetAsync()
        {
            return await _context.ApprovalRequests.AsNoTracking().ToListAsync();
        }

        // Get the approval request by id
        public async Task<ApprovalRequestEntity> GetByIdAsync(Guid id)
        {
            return await _context.ApprovalRequests.AsNoTracking().FirstOrDefaultAsync(ar => ar.Id == id);
        }

        // Approve the request
        public async Task ApproveRequestAsync(Guid id, Guid leaveRequstId, Guid approverId, Guid employeeId)
        {
            var approvalRequest = await _context.ApprovalRequests.FirstOrDefaultAsync(ar => ar.Id == id);

            approvalRequest.Status = "Approved";
            approvalRequest.ApproverId = approverId;

            var leaveRequst = approvalRequest.LeaveRequest;
            leaveRequst.Status = "Approved";

            var employee = approvalRequest.LeaveRequest.Employee;

            var daysRequested = (leaveRequst.EndDate - leaveRequst.StartDate).Days + 1;
            employee.OutOfOfficeBalance -= daysRequested;

            await _context.SaveChangesAsync();
        }

        // Reject the request
        public async Task RejectRequestAsync(Guid id, Guid leaveRequstId, Guid approverId, string comment)
        {
            var approvalRequest = await _context.ApprovalRequests.FirstOrDefaultAsync(ar => ar.Id == id);

            approvalRequest.ApproverId = approverId;
            approvalRequest.Status = "Rejected";
            approvalRequest.Comment = comment;

            var leaveRequst = approvalRequest.LeaveRequest;
            leaveRequst.Status = "Rejected";

            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ApprovalRequestEntity approvalRequest)
        {
            _context.ApprovalRequests.Add(approvalRequest);
            await _context.SaveChangesAsync();
        }
    }
}
