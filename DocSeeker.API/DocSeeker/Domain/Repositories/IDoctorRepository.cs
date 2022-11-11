using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> ListAsync();
        Task AddAsync(Doctor doctor);
        Task<Doctor> FindById(int id);
        void Update(Doctor doctor);
        void Remove(Doctor doctor);        
    }
}
