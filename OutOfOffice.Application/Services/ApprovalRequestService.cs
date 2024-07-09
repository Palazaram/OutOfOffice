using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Application.Services
{
    public class ApprovalRequestService
    {
        private readonly IApprovalRequestStore? _approvalRequestStore;

        public ApprovalRequestService(IApprovalRequestStore approvalRequest)
        {
            _approvalRequestStore = approvalRequest;
        }

        public async Task<IEnumerable<ApprovalRequestEntity>> Get()
        {
            return await _approvalRequestStore.GetAsync();
        }

        public async Task<ApprovalRequestEntity> GetById(Guid id)
        {
            return await _approvalRequestStore.GetByIdAsync(id);
        }

        public async Task ApproveRequest(Guid id, Guid leaveRequstId, Guid approverId, Guid employeeId)
        {
            await _approvalRequestStore.ApproveRequestAsync(id, leaveRequstId, approverId, employeeId);
        }

        public async Task RejectRequest(Guid id, Guid leaveRequstId, Guid approverId, string comment)
        {
            await _approvalRequestStore.RejectRequestAsync(id, leaveRequstId, approverId, comment);
        }

        public async Task Add(ApprovalRequestEntity approvalRequest)
        {
            await _approvalRequestStore.AddAsync(approvalRequest);
        }
    }
}
