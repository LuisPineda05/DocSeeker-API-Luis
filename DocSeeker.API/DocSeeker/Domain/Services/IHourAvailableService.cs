using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IHourAvailableService
    {
        Task<IEnumerable<HourAvailable>> ListAsync();
        Task<HourAvailableResponse> SaveAsync(HourAvailable hourAvailable);
        Task<HourAvailableResponse> UpdateAsync(int id, HourAvailable hourAvailable);
        Task<HourAvailableResponse> DeleteAsync(int id);
    }
}
