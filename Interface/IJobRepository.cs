using JopSy.Models;

namespace JopSy.Interface
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAll();
        public Task<IEnumerable<Job>> GetByUserIdAsync(string userId);
        Task<Job> GetByIdAsyncNoTracking(int id);
        public Task<Job> GetByIdAsync(int id);
        bool Add(Job job);
        bool UpDate(Job job);
        bool Delete(Job job);
        bool Save();
    }
}
