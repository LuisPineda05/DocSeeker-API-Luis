
using DocSeeker.API.Security.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class Doctor : User
    {
        //public int Id { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public int Patients { get; set; }
        public int Years { get; set; }
        public int Age { get; set; }
        public double Cost { get; set; }


        public IList<Date> CDates { get; set; } = new List<Date>();
        public IList<HourAvailable> CHoursAvailable { get; set; } = new List<HourAvailable>();

    }
}
