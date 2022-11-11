using DocSeeker.API.Security.Domain.Models;
using DocSeeker.API.Security.Domain.Services.Communication;

namespace DocSeeker.API.Security.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task RegisterAsync(RegisterRequest model);
    Task UpdateAsync(int id, UpdateRequest model);
    Task DeleteAsync(int id);
}
