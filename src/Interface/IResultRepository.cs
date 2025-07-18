using src.ViewModels;

namespace src.Interface
{
    public interface IResultRepository
    {
        Task<bool> CreateResult(List<ResultViewModel> model);
        Task<ResultViewModel> ViewResult(int id);
        Task<IEnumerable<ResultViewModel>> ViewAllResult();
    }
}
