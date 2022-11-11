namespace DocSeeker.API.Security.Domain.Services.Communication;


public class UpdateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Password { get; set; }
    public string Genre { get; set; }
    public string Birthday { get; set; }
    public string Email { get; set; }
    public string Cell1 { get; set; }
    public string Photo { get; set; }
}
