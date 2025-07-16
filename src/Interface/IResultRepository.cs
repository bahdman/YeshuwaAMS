using src.ViewModels;

namespace src.Interface
{
    public interface IResultRepository
    {
        Task<bool> CreateResult(ResultViewModel model);
        Task<ResultViewModel> ViewResult();
        Task<ICollection<ResultViewModel>> ViewAllResult();
    }
}
