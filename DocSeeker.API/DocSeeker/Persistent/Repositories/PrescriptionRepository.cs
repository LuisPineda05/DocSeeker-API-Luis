using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.Docseeker.Persistent.Repositories;

public class PrescriptionRepository : BaseRepository, IPrescriptionRepository
{
    public PrescriptionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Prescription>> ListAsync()
    {
        return await _context.Prescriptions.ToListAsync();
    }

    public async Task AddAsync(Prescription _prescription)
    {
        await _context.Prescriptions.AddAsync(_prescription);
    }

    public async Task<Prescription> FindById(int id)
    {
        return await _context.Prescriptions.FindAsync(id);
    }

    public void Update(Prescription _prescription)
    {
        _context.Prescriptions.Update(_prescription);
    }

    public void Remove(Prescription _prescription)
    {
        _context.Prescriptions.Remove(_prescription);
    }
}


