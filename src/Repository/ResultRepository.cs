using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Interface;
using src.Models;
using src.ViewModels;

namespace src.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;

        public ResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateResult(List<ResultViewModel> model)
        {
            var results = new List<Result>();
            results.AddRange(model.Select(m => new Result()
            {
                CaScore = m.CaScore,
                ExamScore = m.ExamScore,
                Grade = m.Grade,
                Status = m.Status
            }));           

             await _context.Results.AddRangeAsync(results);
            var dbResponse = await _context.SaveChangesAsync();

            return dbResponse > 0 ? true : false;
        }

        public async Task<IEnumerable<ResultViewModel>> ViewAllResult()
        {
            var items =  await _context.Results.ToListAsync();
            var response = items.Select(m => new ResultViewModel
            {
                CaScore = m.CaScore,
                ExamScore = m.ExamScore,
                Grade = m.Grade,
                Status = m.Status
            });

            return response;
        }

        public async Task<ResultViewModel> ViewResult(int id)
        {
            var item = await _context.Results.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (item == null)
                return new ResultViewModel();

            var response = new ResultViewModel
            {
                CaScore = item.CaScore,
                ExamScore = item.ExamScore,
                Grade = item.Grade,
                Status = item.Status
            };

            return response;
        }
    }
}
