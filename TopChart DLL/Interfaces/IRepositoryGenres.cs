using System.Collections;
using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IRepositoryGenres
    {
        Task<List<Genre>> GetGenresList();
        IEnumerable GetValues();

        Genre GetGenre(int? id);
        Task Create(Genre item);
        Task Delete(int id);
        Task Save();
    }
}
