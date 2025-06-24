using JopSy.Models;

namespace JopSy.Interface
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAll();
        //Task<IEnumerable<Job>> GetClupByCity(string city);
        Task<Job> GetByIdAsync(int id);
        Task<Job> GetByIdAsyncNoTracking(int id);

        bool Add(Job job);
        bool UpDate(Job job);
        bool Delete(Job job);
        bool Save();
    }
}
