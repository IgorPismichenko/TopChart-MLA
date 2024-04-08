using AutoMapper;
using TopChart_BLL.DTO;
using TopChart_BLL.Infrastructure;
using TopChart_BLL.Interfaces;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class VideoService:IVideoService
    {
        IUnitOfWork Database { get; set; }
        public VideoService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<VideoDTO>> GetVideoList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Video, VideoDTO>().ForMember("Singer", opt => opt.MapFrom(c => c.Singer))
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre)));
            var mapper = new Mapper(config);
            return mapper.Map<List<Video>, List<VideoDTO>>(await Database.Videos.GetVideoList());
        }
        public IQueryable<VideoDTO> GetSearchList(string name)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Video, VideoDTO>().ForMember("Singer", opt => opt.MapFrom(c => c.Singer))
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<Video>, IQueryable<VideoDTO>>(Database.Videos.GetSearchList(name));
        }
        public async Task<VideoDTO> GetTrack(int? id)
        {
            var item = Database.Videos.GetTrack(id);
            if (item == null)
                throw new ValidationException("Wrong track!", "");
            return new VideoDTO
            {
                Id = item.Id,
                Name = item.Name,
                SingerId = item.SingerId,
                Singer = item.Singer,
                Album = item.Album,
                GenreId = item.GenreId,
                Genre = item.Genre,
                Like = item.Like,
                Path = item.Path,
                Date = item.Date,
                Size = item.Size
            };
        }
        public async Task Update(VideoDTO item)
        {
            var track = Database.Videos.GetTrack(item.Id);
            track.Id = item.Id;
            track.Name = item.Name;
            track.SingerId = item.SingerId;
            track.Singer = item.Singer;
            track.Album = item.Album;
            track.GenreId = item.GenreId;
            track.Genre = item.Genre;
            track.Like = item.Like;
            track.Path = item.Path;
            track.Date = item.Date;
            track.Size = item.Size;
            Database.Videos.Update(track);
        }
        public async Task Create(VideoDTO item)
        {
            var track = new Video
            {
                Id = item.Id,
                Name = item.Name,
                SingerId = item.SingerId,
                Album = item.Album,
                GenreId = item.GenreId,
                Like = item.Like,
                Path = item.Path,
                Date = item.Date,
                Size = item.Size
            };
            await Database.Videos.Create(track);
        }
        public async Task Delete(int id)
        {
            await Database.Videos.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
