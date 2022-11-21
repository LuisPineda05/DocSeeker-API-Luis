using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.DocSeeker.Persistent.Repositories;

public class MedicalInformationRepository : BaseRepository, IMedicalInformationRepository
{
    public MedicalInformationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MedicalInformation>> ListAsync()
    {
        return await _context.MedicalInformations.ToListAsync();
    }

    public async Task AddAsync(MedicalInformation medicalInformation)
    {
        await _context.MedicalInformations.AddAsync(medicalInformation);
    }

    public async Task<MedicalInformation> FindById(int id)
    {
        return await _context.MedicalInformations.FindAsync(id);
    }

    public void Update(MedicalInformation medicalInformation)
    {
        _context.MedicalInformations.Update(medicalInformation);
    }

    public void Remove(MedicalInformation medicalInformation)
    {
        _context.MedicalInformations.Remove(medicalInformation);
    }
}


