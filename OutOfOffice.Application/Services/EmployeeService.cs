using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStore? _employeeStore;

        public EmployeeService(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        public async Task<IEnumerable<EmployeeEntity>> Get()
        {
            return await _employeeStore.GetAsync();
        }

        public async Task<EmployeeEntity> GetById(Guid id)
        {
            return await _employeeStore.GetByIdAsync(id);
        }

        public async Task Add(EmployeeEntity employeeEntity)
        {
            await _employeeStore.AddAsync(employeeEntity);
        }

        public async Task Update(EmployeeEntity employeeEntity)
        {
            await _employeeStore.UpdateAsync(employeeEntity);
        }

        public async Task DeletePhoto(EmployeeEntity employeeEntity)
        {
            await _employeeStore.DeletePhotoAsync(employeeEntity);
        }

        public async Task AssignProjectsToEmployee(Guid id, List<Guid> projectIds)
        {
            await _employeeStore.AssignProjectsToEmployeeAsync(id, projectIds);
        }
    }
}
