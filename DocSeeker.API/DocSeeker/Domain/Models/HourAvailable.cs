namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class HourAvailable
    {
        public int Id { get; set; }

        public string Hours { get; set; }
        public bool Booked { get; set; }

        public int DoctorId { get; set; }
        public Doctor CDoctor { get; set; }

    }
}
