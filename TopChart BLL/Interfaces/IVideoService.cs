using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_DLL.Entities;

namespace TopChart_BLL.Interfaces
{
    public interface IVideoService
    {
        Task<List<VideoDTO>> GetVideoList();
        IQueryable<VideoDTO> GetSearchList(string name);
        Task<VideoDTO> GetTrack(int? id);
        Task Update(VideoDTO item);
        Task Create(VideoDTO item);
        Task Delete(int id);
        Task Save();
    }
}
