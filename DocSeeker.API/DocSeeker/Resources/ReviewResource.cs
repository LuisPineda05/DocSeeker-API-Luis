using DocSeeker.API.DocSeeker.Domain.Models;

namespace DocSeeker.API.DocSeeker.Resources
{
    public class ReviewResource
    {
        public int Id { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public string CustomerReview { get; set; }

        public string CustomerName { get; set; }

        public int CustomerScore { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}
