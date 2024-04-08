using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{ 
    public interface IRepositoryComments
    {
        Task<List<Comment>> GetCommentList();
        Task Create(Comment item);
        Task Delete(int id);
        Task Save();
    }
}
