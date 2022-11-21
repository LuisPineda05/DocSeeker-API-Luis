using System.Collections.Generic;
using System.Threading.Tasks;
using DocSeeker.API.DocSeeker.Domain.Models;


namespace DocSeeker.API.DocSeeker.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> ListAsync();
        Task AddAsync(Review review);
        Task<Review> FindById(int id);
        void Update(Review review);
        void Remove(Review review);
    }
}
