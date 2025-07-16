using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Interface;
using src.Models;
using src.ViewModels;

namespace src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(User Model)
        {
            _context.Add(Model);
            return Save();
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            // return await _context.Users.Include(i => i.Lecturer).FirstOrDefaultAsync(i => i.Id == id);
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<User> GetByIdAsyncNoTracking(string id)
        {
            // return await _context.Users.Include(i => i.Lecturer).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<StudentViewModel> GetStudentDashboard(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new StudentViewModel();

            var user = await _context.Users.Include(m => m.Department)
            .Where(m => m.Id == userId)
            .FirstOrDefaultAsync();
            
            if (user == null)
                return new StudentViewModel();

            var response = new StudentViewModel
            {
                FullName = user?.FullName ?? "",
                MatricNumber = user?.UserName ?? "",
                Department = user?.Department?.Name ?? "Not assigned",
                Level = user?.Level
            };

            return response;
        }

        public async Task<IEnumerable<User>> GetUser(string search)
        {
            return await _context.Users.Where(c => c.FullName.Contains(search)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User User)
        {
            _context.Update(User);
            return Save();
        }

    }
}
