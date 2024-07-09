using Microsoft.EntityFrameworkCore;
using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Persistence.Repository
{
    public class EmployeeRepository : IEmployeeStore
    {
        private readonly OutOfOfficeDbContext? _context;

        public EmployeeRepository(OutOfOfficeDbContext context)
        {
            _context = context;
        }

        // Get all employees
        public async Task<IEnumerable<EmployeeEntity>> GetAsync()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        // Get the employee by id
        public async Task<EmployeeEntity> GetByIdAsync(Guid id)
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        // Add the employee
        public async Task AddAsync(EmployeeEntity employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        // Update the employee
        public async Task UpdateAsync(EmployeeEntity employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(EmployeeEntity employee)
        {
            employee.PhotoPath = null;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task AssignProjectsToEmployeeAsync(Guid id, List<Guid> projectIds)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            employee.AssignedProjects.Clear();

            foreach (var projectId in projectIds)
            {
                var project = await _context.Projects.FindAsync(projectId);
                if (project != null)
                {
                    employee.AssignedProjects.Add(project);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
