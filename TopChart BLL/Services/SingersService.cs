using AutoMapper;
using System.Collections;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_BLL.Infrastructure;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class SingersService:ISingersService
    {
        IUnitOfWork Database { get; set; }
        public SingersService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<SingerDTO>> GetSingersList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Singer, SingerDTO>()).CreateMapper();
            return mapper.Map<List<Singer>, List<SingerDTO>>(await Database.Singers.GetSingersList());
        }
        public IEnumerable GetValues()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Singer, SingerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Singer>, IEnumerable<SingerDTO>>((IEnumerable<Singer>)Database.Singers.GetValues());
        }
        public SingerDTO GetSinger(int? id)
        {
            var item = Database.Singers.GetSinger(id);
            if (item == null)
                throw new ValidationException("Wrong singer!", "");
            return new SingerDTO
            {
                Id = item.Id,
                Name = item.Name,
                Path = item.Path
            };
        }
        public async Task Create(SingerDTO item)
        {
            var singer = new Singer
            {
                Id = item.Id,
                Name = item.Name,
                Path = item.Path
            };
            await Database.Singers.Create(singer);
        }
        public async Task Delete(int id)
        {
            await Database.Singers.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
