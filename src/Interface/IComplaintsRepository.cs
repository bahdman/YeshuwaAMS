using src.ViewModels;

namespace src.Interface
{
    public interface IComplaintsRepository
    {
        Task<bool> CreateComplaint(ComplaintViewModel model, string userId);
        Task<ComplaintViewModel> ViewComplaint(int id);
        Task<IEnumerable<ComplaintViewModel>> ViewAllComplaint();
    }
}
