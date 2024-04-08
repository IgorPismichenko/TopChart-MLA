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
    public interface IGenresService
    {
        Task<List<GenreDTO>> GetGenresList();
        IEnumerable GetValues();
        GenreDTO GetGenre(int? id);
        Task Create(GenreDTO item);
        Task Delete(int id);
        Task Save();
    }
}
