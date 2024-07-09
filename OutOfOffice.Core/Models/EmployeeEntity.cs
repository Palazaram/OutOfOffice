using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OutOfOffice.Core.Models
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string? FullName { get; set; }

        [Required]
        [Display(Name = "Subdivision")]
        public string? Subdivision { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string? Position { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string? Status { get; set; }

        //[Required]
        [Display(Name = "People Partner")]
        public Guid? PeoplePartnerId { get; set; }

        [Required]
        [Display(Name = "Out-of-Office Balance")]
        public int OutOfOfficeBalance { get; set; }

        [Display(Name = "Photo")]
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }

        public virtual EmployeeEntity? PeoplePartner { get; set; }
        public virtual ICollection<EmployeeEntity>? PeoplePartnerEmployees { get; set; }
        public virtual ICollection<LeaveRequestEntity>? LeaveRequests { get; set; }
        public virtual ICollection<ApprovalRequestEntity>? ApprovalRequests { get; set; }
        public virtual ICollection<ProjectEntity>? Projects { get; set; }
        public virtual ICollection<ProjectEntity>? AssignedProjects { get; set; }
    }
}
