using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Shared.Domain.Services.Communication;

namespace DocSeeker.API.DocSeeker.Domain.Services.Communication
{
    public class ReviewResponse : BaseResponse<Review>
    {
        public ReviewResponse(string message) : base(message)
        {
        }

        public ReviewResponse(Review resource) : base(resource)
        {
        }
    }
}
