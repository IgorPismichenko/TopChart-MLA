using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_DLL.Entities;

namespace TopChart_BLL.Interfaces
{
    public interface ISingersService
    {
        Task<List<SingerDTO>> GetSingersList();
        IEnumerable GetValues();

        SingerDTO GetSinger(int? id);
        Task Create(SingerDTO item);
        Task Delete(int id);
        Task Save();
    }
}
