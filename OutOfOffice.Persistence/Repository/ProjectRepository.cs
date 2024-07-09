using Microsoft.EntityFrameworkCore;
using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Persistence.Repository
{
    public class ProjectRepository : IProjectStore
    {
        private readonly OutOfOfficeDbContext? _context;

        public ProjectRepository(OutOfOfficeDbContext context)
        {
            _context = context;
        }

        // Get all projects
        public async Task<IEnumerable<ProjectEntity>> GetAsync()
        {
            return await _context.Projects.AsNoTracking().ToListAsync();
        }

        // Get the project by id
        public async Task<ProjectEntity> GetByIdAsync(Guid id)
        {
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        // Add the project
        public async Task AddAsync(ProjectEntity project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        // Update the project
        public async Task UpdateAsync(ProjectEntity project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
