using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<Prescription>> ListAsync();
        Task<PrescriptionResponse> SaveAsync(Prescription prescription);
        Task<PrescriptionResponse> UpdateAsync(int id, Prescription prescription);
        Task<PrescriptionResponse> DeleteAsync(int id);
    }
}
