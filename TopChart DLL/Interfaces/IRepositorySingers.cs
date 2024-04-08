using System.Collections;
using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IRepositorySingers
    {
        Task<List<Singer>> GetSingersList();
        IEnumerable GetValues();

        Singer GetSinger(int? id);
        Task Create(Singer item);
        Task Delete(int id);
        Task Save();
    }
}
