using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Core.Models
{
    public class ApprovalRequestEntity
    {
        public Guid Id { get; set; }

        //[Required]
        public Guid? ApproverId { get; set; }

        [Required]
        public Guid LeaveRequestId { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "New";

        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        public virtual EmployeeEntity? Approver { get; set; }
        public virtual LeaveRequestEntity? LeaveRequest { get; set; }
    }
}
