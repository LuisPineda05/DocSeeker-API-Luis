﻿using System.Text.Json.Serialization;

namespace DocSeeker.API.Security.Domain.Models;

public abstract class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dni { get; set; }
    public string Genre { get; set; }
    public string Birthday { get; set; }
    public string Email { get; set; }
    public string cell1 { get; set; }
    public string photo { get; set; }
    public string Password { get; set; }

   //public IList<Dates> Dates { get; set; }
}
