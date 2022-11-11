using DocSeeker.API.Security.Resources;
using System.Collections.Generic;
using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Resources
{
    public class DoctorResource : UserResource
    {
        //public int Id { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public int Patients { get; set; }
        public int Years { get; set; }
        public int Age { get; set; }
        public double Cost { get; set; }

       // public IList<Date> CDates { get; set; }

    }
}
