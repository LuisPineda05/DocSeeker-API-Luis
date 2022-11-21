using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.DocSeeker.Domain.Services.Communication;
using System.Numerics;

namespace DocSeeker.API.DocSeeker.Domain.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<ReviewResponse> SaveAsync(Review category);
        Task<ReviewResponse> UpdateAsync(int id, Review category);
        Task<ReviewResponse> DeleteAsync(int id);
    }
}
