using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_DLL.Entities;

namespace TopChart_BLL.Interfaces
{
    public interface ICommentsService
    {
        Task<List<CommentDTO>> GetCommentList();
        Task Create(CommentDTO item);
        Task Delete(int id);
        Task Save();
    }
}
