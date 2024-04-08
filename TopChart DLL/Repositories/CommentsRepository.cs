using Microsoft.EntityFrameworkCore;
using TopChart_DLL.EF;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class CommentsRepository: IRepositoryComments
    {
        private readonly TopChartDbMLAContext _context;

        public CommentsRepository(TopChartDbMLAContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentList()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task Create(Comment c)
        {
            await _context.Comment.AddAsync(c);
        }
        public async Task Delete(int id)
        {
            Comment? c = await _context.Comment.FindAsync(id);
            if (c != null)
                _context.Comment.Remove(c);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
