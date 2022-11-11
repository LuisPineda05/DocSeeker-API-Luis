using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using System.Numerics;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IDateService
    {
        Task<IEnumerable<Date>> ListAsync();
        Task<DateResponse> SaveAsync(Date category);
        Task<DateResponse> UpdateAsync(int id, Date category);
        Task<DateResponse> DeleteAsync(int id);
    }
}
