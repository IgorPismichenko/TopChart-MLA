using AutoMapper;
using System.Collections;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_BLL.Infrastructure;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class GenresService:IGenresService
    {
        IUnitOfWork Database { get; set; }
        public GenresService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<GenreDTO>> GetGenresList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>()).CreateMapper();
            return mapper.Map<List<Genre>, List<GenreDTO>>(await Database.Genres.GetGenresList());
        }
        public IEnumerable GetValues()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>((IEnumerable<Genre>)Database.Genres.GetValues());
        }
        public GenreDTO GetGenre(int? id)
        {
            var item = Database.Genres.GetGenre(id);
            if (item == null)
                throw new ValidationException("Wrong genre!", "");
            return new GenreDTO
            {
                Id = item.Id,
                Name = item.Name
            };
        }
        public async Task Create(GenreDTO item)
        {
            var genre = new Genre
            {
                Id = item.Id,
                Name= item.Name
            };
            await Database.Genres.Create(genre);
        }
        public async Task Delete(int id)
        {
            await Database.Genres.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
