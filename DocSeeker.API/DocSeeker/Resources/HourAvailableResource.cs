namespace DocSeeker.API.DocSeeker.Resources
{
    public class HourAvailableResource
    {
        public int Id { get; set; }

        public string Hours { get; set; }
        public bool Booked { get; set; }

        public int DoctorId { get; set; }
       // public Doctor CDoctor { get; set; }
    }
}
