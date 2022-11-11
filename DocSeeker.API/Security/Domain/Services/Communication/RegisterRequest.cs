using System.ComponentModel.DataAnnotations;

namespace DocSeeker.API.Security.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Dni { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public string Birthday { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string cell1 { get; set; }

    public string photo { get; set; }
}
