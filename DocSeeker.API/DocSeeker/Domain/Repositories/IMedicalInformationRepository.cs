using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IMedicalInformationRepository
    {
        Task<IEnumerable<MedicalInformation>> ListAsync();
        Task AddAsync(MedicalInformation medicalInformation);
        Task<MedicalInformation> FindById(int id);
        void Update(MedicalInformation medicalInformation);
        void Remove(MedicalInformation medicalInformation);
    }
}


