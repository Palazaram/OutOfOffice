using OutOfOffice.Core.Models;

namespace OutOfOffice.Core.Stores
{
    public interface IApprovalRequestStore
    {
        Task<IEnumerable<ApprovalRequestEntity>> GetAsync();
        Task<ApprovalRequestEntity> GetByIdAsync(Guid id);
        Task ApproveRequestAsync(Guid id, Guid leaveRequstId, Guid approverId, Guid employeeId);
        Task RejectRequestAsync(Guid id, Guid leaveRequstId, Guid approverId, string comment);
        Task AddAsync(ApprovalRequestEntity approvalRequest);
    }
}
