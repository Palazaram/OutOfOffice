using OutOfOffice.Core.Models;

namespace OutOfOffice.Core.Stores
{
    public interface IProjectStore
    {
        Task<IEnumerable<ProjectEntity>> GetAsync();
        Task<ProjectEntity> GetByIdAsync(Guid id);
        Task AddAsync(ProjectEntity project);
        Task UpdateAsync(ProjectEntity project);
    }
}
