using DocSeeker.API.Security.Resources;
using System.Collections.Generic;
using DocSeeker.API.DocSeeker.Domain.Models;


namespace DocSeeker.API.DocSeeker.Resources
{
    public class DateResource
    {
        public int Id { get; set; }


        public int IdPatient { get; set; }

        public int DoctorId { get; set; }

       // public PatientResource? CPatient { get; set; }
        
        //public DoctorResource? CDoctor { get; set; }


        public string? CDate { get; set; }
    }
}
