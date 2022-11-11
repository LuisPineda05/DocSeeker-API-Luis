using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface INewService
    {
        Task<IEnumerable<New>> ListAsync();
        Task<NewResponse> SaveAsync(New category);
        Task<NewResponse> UpdateAsync(int id, New category);
        Task<NewResponse> DeleteAsync(int id);
    }
}
