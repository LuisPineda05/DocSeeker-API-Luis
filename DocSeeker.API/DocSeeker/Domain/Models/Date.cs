namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class Date
    {
        public int Id { get; set; }
        
        public int IdPatient { get; set; }
        public Patient CPatient { get; set; }


        public int DoctorId { get; set; }
        public Doctor CDoctor { get; set; }

        public string CDate { get; set; }

        // hour id
    }
}