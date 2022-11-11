using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;


namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> ListAsync();
        Task AddAsync(Patient patient);
        Task<Patient> FindById(int id);
        void Update(Patient patient);
        void Remove(Patient patient);
    }
}
