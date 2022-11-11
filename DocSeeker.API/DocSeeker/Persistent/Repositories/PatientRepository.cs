using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.DocSeeker.Persistent.Repositories;

public class PatientRepository : BaseRepository, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Patient>> ListAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task AddAsync(Patient category)
    {
        await _context.Patients.AddAsync(category);
    }

    public async Task<Patient> FindById(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public void Update(Patient category)
    {
        _context.Patients.Update(category);
    }

    public void Remove(Patient category)
    {
        _context.Patients.Remove(category);
    }
}

