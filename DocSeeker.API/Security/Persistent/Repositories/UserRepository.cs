using DocSeeker.API.Security.Domain.Models;
using DocSeeker.API.Security.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.Security.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User> FindByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> FindByDniAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Dni == username);
    }

    public User FindById(int id)
    {
        return _context.Users.Find(id);
    }

    public bool ExistsByDni(string username)
    {
        return _context.Users.Any(x => x.Dni == username);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}