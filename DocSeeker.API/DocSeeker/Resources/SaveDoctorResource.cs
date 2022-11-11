using DocSeeker.API.Security.Resources;

namespace DocSeeker.API.DocSeeker.Resources
{
    public class SaveDoctorResource : SaveUserResource
    {
        public string Area { get; set; }
        public string Description { get; set; }
        public int Patients { get; set; }
        public int Years { get; set; }
        public int Age { get; set; }
        public double Cost { get; set; }        
    }
}
