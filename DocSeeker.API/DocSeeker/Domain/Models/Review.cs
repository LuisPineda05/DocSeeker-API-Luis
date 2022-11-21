namespace DocSeeker.API.DocSeeker.Domain.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public string CustomerReview { get; set; }

        public string CustomerName { get; set; }

        public int CustomerScore { get; set; }

        public int IdPatient { get; set; }
        public Patient CPatient { get; set; }


        public int IdDoctor { get; set; }
        public Doctor CDoctor { get; set; }


    }
}