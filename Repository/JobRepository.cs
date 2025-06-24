using JopSy.Data;
using JopSy.Interface;
using JopSy.Models;
using Microsoft.EntityFrameworkCore;

namespace JopSy.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public bool Add(Job job)
        {
            _context.Add(job);
            return Save();
        }

        public bool Delete(Job job)
        {
            _context.Remove(job);
            return Save();
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await _context.Jobs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Job> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Jobs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpDate(Job job)
        {
            _context.Update(job);
            return Save();
        }
    }
}
