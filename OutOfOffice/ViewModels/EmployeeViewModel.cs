using OutOfOffice.Core.Models;

namespace OutOfOffice.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeEntity Employee { get; set; }
        public IEnumerable<ProjectEntity> Projects { get; set; }
        public IEnumerable<Guid> SelectedProjects { get; set; } = new List<Guid>();
    }
}
