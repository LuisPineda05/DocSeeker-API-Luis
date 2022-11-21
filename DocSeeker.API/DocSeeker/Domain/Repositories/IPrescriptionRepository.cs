using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;


namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> ListAsync();
        Task AddAsync(Prescription prescription);
        Task<Prescription> FindById(int id);
        void Update(Prescription prescription);
        void Remove(Prescription prescription);
    }
}
