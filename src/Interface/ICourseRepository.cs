using src.Models;

namespace src.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<IEnumerable<Course>> GetCourse(string search);
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdAsyncNoTracking(int id);
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Course course);
        bool Save();
    }
}
