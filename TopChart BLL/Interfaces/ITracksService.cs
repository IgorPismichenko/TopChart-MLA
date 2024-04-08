using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_DLL.Entities;

namespace TopChart_BLL.Interfaces
{
    public interface ITracksService
    {
        Task<List<TracksDTO>> GetTracksList();
        IQueryable<TracksDTO> GetSearchList(string name);
        Task<TracksDTO> GetTrack(int? id);
        Task Create(TracksDTO item);
        Task Delete(int id);
        Task Update(TracksDTO item);
        Task Save();
    }
}
