using OutOfOffice.Core.Models;

namespace OutOfOffice.Core.Stores
{
    public interface ILeaveRequestStore
    {
        Task<IEnumerable<LeaveRequestEntity>> GetAsync();
        Task<LeaveRequestEntity> GetByIdAsync(Guid id);
        Task AddAsync(LeaveRequestEntity leaveRequest);
        Task UpdateAsync(LeaveRequestEntity leaveRequest);
        Task SubmitLeaveRequestAsync(Guid id);
        Task CancelLeaveRequestAsync(Guid id);
    }
}
