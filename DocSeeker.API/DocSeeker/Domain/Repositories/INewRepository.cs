using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Repositories;

public interface INewRepository
{
    Task<IEnumerable<New>> ListAsync();
    Task AddAsync(New _new);
    Task<New> FindById(int id);
    void Update(New _new);
    void Remove(New _new);
}
