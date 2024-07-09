using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Application.Services
{
    public class LeaveRequestService
    {
        private readonly ILeaveRequestStore? _leaveRequestStore;

        public LeaveRequestService(ILeaveRequestStore leaveRequest)
        {
            _leaveRequestStore = leaveRequest;
        }

        public async Task<IEnumerable<LeaveRequestEntity>> Get()
        {
            return await _leaveRequestStore.GetAsync();
        }

        public async Task<LeaveRequestEntity> GetById(Guid id)
        {
            return await _leaveRequestStore.GetByIdAsync(id);
        }

        public async Task Add(LeaveRequestEntity leaveRequest)
        {
            await _leaveRequestStore.AddAsync(leaveRequest);
        }

        public async Task Update(LeaveRequestEntity leaveRequest)
        {
            await _leaveRequestStore.UpdateAsync(leaveRequest);
        }

        public async Task SubmitLeaveRequest(Guid id)
        {
            await _leaveRequestStore.SubmitLeaveRequestAsync(id);
        }

        public async Task CancelLeaveRequest(Guid id)
        {
            await _leaveRequestStore.CancelLeaveRequestAsync(id);
        }
    }
}
