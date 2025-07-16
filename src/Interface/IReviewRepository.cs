using src.ViewModels;

namespace src.Interface
{
    public interface IReviewRepository
    {
        Task<bool> CreateReview(ReviewViewModel model, string userId);
        Task<ReviewViewModel> ViewReview(int id);
        Task<IEnumerable<ReviewViewModel>> ViewAllReview();
    }
}
