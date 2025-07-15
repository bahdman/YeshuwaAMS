using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Interface;
using src.Models;

namespace src.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Course course)
        {
            _context.Add(course);
            return Save();
        }

        public bool Delete(Course course)
        {
            _context.Remove(course);
            return Save();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.Include(i => i.Lecturer).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Course> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Courses.Include(i => i.Lecturer).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Course>> GetCourse(string search)
        {
            return await _context.Courses.Where(c => c.Name.Contains(search)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Course course)
        {
            _context.Update(course);
            return Save();
        }
    }
}
