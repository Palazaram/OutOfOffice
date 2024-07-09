using OutOfOffice.Core.Models;

namespace OutOfOffice.Core.Stores
{
    public interface IEmployeeStore
    {
        Task<IEnumerable<EmployeeEntity>> GetAsync();
        Task<EmployeeEntity> GetByIdAsync(Guid id);
        Task AddAsync(EmployeeEntity employee);
        Task UpdateAsync(EmployeeEntity employee);
        Task DeletePhotoAsync(EmployeeEntity employee);
        Task AssignProjectsToEmployeeAsync(Guid id, List<Guid> projectIds);
    }
}
