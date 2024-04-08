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
    public class CommentsService:ICommentsService
    {
        IUnitOfWork Database { get; set; }
        public CommentsService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<CommentDTO>> GetCommentList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comment, CommentDTO>()).CreateMapper();
            return mapper.Map<List<Comment>, List<CommentDTO>>(await Database.Comments.GetCommentList());
        }
        public async Task Create(CommentDTO item)
        {
            var comment = new Comment
            {
                Id = item.Id,
                Message = item.Message,
                UserId = item.UserId,
                TrackId = item.TrackId,
                Date = item.Date
            };
            await Database.Comments.Create(comment);
        }
        public async Task Delete(int id)
        {
            await Database.Comments.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
