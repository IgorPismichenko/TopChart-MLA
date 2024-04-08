using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_BLL.Infrastructure;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class TracksService:ITracksService
    {
        IUnitOfWork Database { get; set; }
        public TracksService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<TracksDTO>> GetTracksList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tracks, TracksDTO>().ForMember("Singer", opt => opt.MapFrom(c => c.Singer))
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre)));
            var mapper = new Mapper(config);
            return mapper.Map<List<Tracks>, List<TracksDTO>>(await Database.Tracks.GetTracksList());
        }
        
        public IQueryable<TracksDTO> GetSearchList(string name)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Tracks, TracksDTO>().ForMember("Singer", opt => opt.MapFrom(c => c.Singer))
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre)));
            var mapper = new Mapper(config);
            return mapper.Map<IQueryable<Tracks>, IQueryable<TracksDTO>>(Database.Tracks.GetSearchList(name));
        }
        public async Task<TracksDTO> GetTrack(int? id)
        {
            var item = Database.Tracks.GetTrack(id);
            if (item == null)
                throw new ValidationException("Wrong track!", "");
            return new TracksDTO
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
        public async Task Create(TracksDTO item)
        {
            var track = new Tracks
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
            await Database.Tracks.Create(track);
        }
        public async Task Update(TracksDTO item)
        {
            var track = Database.Tracks.GetTrack(item.Id);
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
            Database.Tracks.Update(track);
        }
        public async Task Delete(int id)
        {
            await Database.Tracks.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
