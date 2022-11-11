using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IHourAvailableRepository
    {
        Task<IEnumerable<HourAvailable>> ListAsync();
        Task AddAsync(HourAvailable hourAvailable);
        Task<HourAvailable> FindById(int id);
        void Update(HourAvailable hourAvailable);
        void Remove(HourAvailable hourAvailable);
    }
}
