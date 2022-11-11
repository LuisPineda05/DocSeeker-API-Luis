using DocSeeker.API.Security.Domain.Models;

namespace DocSeeker.API.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByDniAsync(string username);
    public bool ExistsByDni(string username);
    User FindById(int id);
    void Update(User user);
    void Remove(User user);
}