using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.DocSeeker.Persistent.Repositories;

public class HourAvailableRepository : BaseRepository, IHourAvailableRepository
{
    public HourAvailableRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<HourAvailable>> ListAsync()
    {
        return await _context.HoursAvailable.ToListAsync();
    }

    public async Task AddAsync(HourAvailable doctor)
    {
        await _context.HoursAvailable.AddAsync(doctor);
    }

    public async Task<HourAvailable> FindById(int id)
    {
        return await _context.HoursAvailable.FindAsync(id);
    }

    public void Update(HourAvailable doctor)
    {
        _context.HoursAvailable.Update(doctor);
    }

    public void Remove(HourAvailable doctor)
    {
        _context.HoursAvailable.Remove(doctor);
    }
}

