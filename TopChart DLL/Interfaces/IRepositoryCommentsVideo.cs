using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IRepositoryCommentsVideo
    {
        Task<List<CommentVideo>> GetCommentList();
        Task Create(CommentVideo item);
        Task Delete(int id);
        Task Save();
    }
}
