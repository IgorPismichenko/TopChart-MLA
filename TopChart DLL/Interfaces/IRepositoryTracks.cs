using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IRepositoryTracks
    {
        Task<List<Tracks>> GetTracksList();
        Task<List<Tracks>> GetSortedTracksList();
        IQueryable<Tracks> GetSearchList(string name);
        Tracks GetTrack(int? id);
        Task Create(Tracks item);
        Task Delete(int id);
        void Update(Tracks t);
        Task Save();
    }
}
