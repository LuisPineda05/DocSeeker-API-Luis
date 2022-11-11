using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Repositories;

public interface IDateRepository
{
    Task<IEnumerable<Date>> ListAsync();
    Task AddAsync(Date date);
    Task<Date> FindById(int id);
    void Update(Date date);
    void Remove(Date date);
}
