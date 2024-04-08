using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryComments Comments { get; }
        IRepositoryCommentsVideo CommentsVideo { get; }
        IRepositoryGenres Genres { get; }
        IRepositorySingers Singers { get; }
        IRepositoryTracks Tracks { get; }
        IRepositoryUsers Users { get; }
        IRepositoryVideo Videos { get; }
        Task Save();
    }
}
