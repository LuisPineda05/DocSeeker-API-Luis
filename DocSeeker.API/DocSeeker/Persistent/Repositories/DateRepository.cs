using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.DocSeeker.Persistent.Repositories
{
    public class DateRepository : BaseRepository, IDateRepository
    {
        public DateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Date>> ListAsync()
        {
            return await _context.Dates.ToListAsync();
        }

        public async Task AddAsync(Date date)
        {
            await _context.Dates.AddAsync(date);
        }

        public async Task<Date> FindById(int id)
        {
            return await _context.Dates.FindAsync(id);
        }

        public void Update(Date date)
        {
            _context.Dates.Update(date);
        }

        public void Remove(Date date)
        {
            _context.Dates.Remove(date);
        }
    }
}
