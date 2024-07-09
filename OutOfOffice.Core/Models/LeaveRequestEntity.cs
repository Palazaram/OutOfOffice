using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Core.Models
{
    public class LeaveRequestEntity
    {
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; } // FK from the “Employee”

        [Required]
        [Display(Name = "Absence Reason")]
        public string? AbsenceReason { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "New";

        public virtual EmployeeEntity? Employee { get; set; }
        public virtual ICollection<ApprovalRequestEntity>? ApprovalRequests { get; set; }
    }
}
