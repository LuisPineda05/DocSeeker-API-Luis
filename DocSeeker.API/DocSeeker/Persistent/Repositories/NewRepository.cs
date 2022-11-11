using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.Docseeker.Persistent.Repositories;

public class NewRepository : BaseRepository, INewRepository
{
    public NewRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<New>> ListAsync()
    {
        return await _context.News.ToListAsync();
    }

    public async Task AddAsync(New _new)
    {
        await _context.News.AddAsync(_new);
    }

    public async Task<New> FindById(int id)
    {
        return await _context.News.FindAsync(id);
    }

    public void Update(New _new)
    {
        _context.News.Update(_new);
    }

    public void Remove(New _new)
    {
        _context.News.Remove(_new);
    }
}


