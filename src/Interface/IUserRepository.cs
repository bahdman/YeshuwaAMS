using src.Models;
using src.ViewModels;

namespace src.Interface
{
    public interface IUserRepository
    {
        bool Add(User model);
        Task<StudentViewModel> GetStudentDashboard(string userId);
    }
}
