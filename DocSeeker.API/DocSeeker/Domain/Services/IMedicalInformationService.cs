using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IMedicalInformationService
    {
        Task<IEnumerable<MedicalInformation>> ListAsync();
        Task<MedicalInformationResponse> SaveAsync(MedicalInformation medicalInformation);
        Task<MedicalInformationResponse> UpdateAsync(int id, MedicalInformation medicalInformation);
        Task<MedicalInformationResponse> DeleteAsync(int id);
    }
}
