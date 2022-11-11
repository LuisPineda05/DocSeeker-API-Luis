namespace DocSeeker.API.DocSeeker.Resources
{
    public class SaveHourAvailableResource
    {
        public string Hours { get; set; }
        public bool Booked { get; set; }
        public int DoctorId { get; set; }
    }
}
