using DocSeeker.API.Security.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class Patient : User
    {
        //public int Id { get; set; }

        public IList<Date> CDates { get; set; } = new List<Date>();
    }
}
