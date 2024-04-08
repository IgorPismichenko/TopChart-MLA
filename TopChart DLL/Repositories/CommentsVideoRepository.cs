using Microsoft.EntityFrameworkCore;
using TopChart_DLL.EF;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class CommentsVideoRepository: IRepositoryCommentsVideo
    {
        private readonly TopChartDbMLAContext _context;

        public CommentsVideoRepository(TopChartDbMLAContext context)
        {
            _context = context;
        }

        public async Task<List<CommentVideo>> GetCommentList()
        {
            return await _context.CommentVideo.ToListAsync();
        }

        public async Task Create(CommentVideo c)
        {
            await _context.CommentVideo.AddAsync(c);
        }
        public async Task Delete(int id)
        {
            CommentVideo? c = await _context.CommentVideo.FindAsync(id);
            if (c != null)
                _context.CommentVideo.Remove(c);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
