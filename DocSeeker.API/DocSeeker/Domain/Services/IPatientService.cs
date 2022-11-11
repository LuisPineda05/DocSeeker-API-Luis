using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> ListAsync();
        Task<PatientResponse> SaveAsync(Patient patient);
        Task<PatientResponse> UpdateAsync(int id, Patient patient);
        Task<PatientResponse> DeleteAsync(int id);
    }
}
