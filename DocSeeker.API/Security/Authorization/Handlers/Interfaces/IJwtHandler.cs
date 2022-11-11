using DocSeeker.API.Security.Domain.Models;

namespace DocSeeker.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}