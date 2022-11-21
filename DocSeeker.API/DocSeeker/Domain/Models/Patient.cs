using DocSeeker.API.Security.Domain.Models;

namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class Patient : User
    {
        //public int Id { get; set; }

        public IList<Date> CDates { get; set; } = new List<Date>();
        public IList<Review> CReviews { get; set; } = new List<Review>();
        public IList<MedicalInformation> CMedicalInformation { get; set; } = new List<MedicalInformation>();
        public IList<Prescription> CPrescription { get; set; } = new List<Prescription>();

    }
}
