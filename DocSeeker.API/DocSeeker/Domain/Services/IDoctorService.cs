using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> ListAsync();
        Task<DoctorResponse> SaveAsync(Doctor category);
        Task<DoctorResponse> UpdateAsync(int id, Doctor category);
        Task<DoctorResponse> DeleteAsync(int id);
    }
}
