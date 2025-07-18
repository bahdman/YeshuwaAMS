using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Interface;
using src.Models;
using src.ViewModels;

namespace src.Repository
{
    public class ComplaintRepository : IComplaintsRepository
    {
        private readonly ApplicationDbContext _context;

        public ComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateComplaint(ComplaintViewModel model, string userId)
        {
            var complaint = new Complaint
            {
                Message = model.Message,
                CreatedBy = userId
            };

            await _context.Complaints.AddAsync(complaint);
            var dbResponse = await _context.SaveChangesAsync();

            return dbResponse > 0 ? true : false;
        }

        public async Task<IEnumerable<ComplaintViewModel>> ViewAllComplaint()
        {
            var items = await _context.Complaints.ToListAsync();
            var response = items.Select(m => new ComplaintViewModel
            {
                Message = m.Message
            });

            return response;
        }

        public async Task<ComplaintViewModel> ViewComplaint(int id)
        {
            var item = await _context.Complaints.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (item == null)
                return new ComplaintViewModel();

            return new ComplaintViewModel { Id = item.Id , Message = item.Message};
        }
    }
}
