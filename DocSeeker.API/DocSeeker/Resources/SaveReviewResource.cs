namespace DocSeeker.API.DocSeeker.Resources
{
    public class SaveReviewResource
    {
        public string ProfilePhotoUrl { get; set; }

        public string CustomerReview { get; set; }

        public string CustomerName { get; set; }

        public int CustomerScore { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}
