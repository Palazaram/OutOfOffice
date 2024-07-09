using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Core.Models
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Project Type")]
        public string? ProjectType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Project Manager")]
        public Guid ProjectManagerId { get; set; }

        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string? Status { get; set; }

        public virtual EmployeeEntity? ProjectManager { get; set; }
        public virtual ICollection<EmployeeEntity>? Employees { get; set; }
    }
}
