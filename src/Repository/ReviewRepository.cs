using src.Interface;
using src.ViewModels;

namespace src.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        public Task<bool> CreateReview(ReviewViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewViewModel>> ViewAllReview()
        {
            throw new NotImplementedException();
        }

        public Task<ReviewViewModel> ViewReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
