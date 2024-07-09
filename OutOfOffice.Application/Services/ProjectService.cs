using OutOfOffice.Core.Models;
using OutOfOffice.Core.Stores;

namespace OutOfOffice.Application.Services
{
    public class ProjectService
    {
        private readonly IProjectStore? _projectStore;

        public ProjectService(IProjectStore projectStore)
        {
            _projectStore = projectStore;
        }

        public async Task<IEnumerable<ProjectEntity>> Get()
        {
            return await _projectStore.GetAsync();
        }

        public async Task<ProjectEntity> GetById(Guid id)
        {
            return await _projectStore.GetByIdAsync(id);
        }

        public async Task Add(ProjectEntity projectEntity)
        {
            await _projectStore.AddAsync(projectEntity);
        }

        public async Task Update(ProjectEntity projectEntity)
        {
            await _projectStore.UpdateAsync(projectEntity);
        }
    }
}
