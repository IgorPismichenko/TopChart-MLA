using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class CommentsVideoService:ICommentsVideoService
    {
        IUnitOfWork Database { get; set; }
        public CommentsVideoService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<CommentVideoDTO>> GetCommentList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CommentVideo, CommentVideoDTO>()).CreateMapper();
            return mapper.Map<List<CommentVideo>, List<CommentVideoDTO>>(await Database.CommentsVideo.GetCommentList());
        }
        public async Task Create(CommentVideoDTO item)
        {
            var comment = new CommentVideo
            {
                Id = item.Id,
                Message = item.Message,
                UserId = item.UserId,
                VideoId = item.VideoId,
                Date = item.Date
            };
            await Database.CommentsVideo.Create(comment);
        }
        public async Task Delete(int id)
        {
            await Database.CommentsVideo.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
