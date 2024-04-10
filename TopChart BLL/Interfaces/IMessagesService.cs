using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;

namespace TopChart_BLL.Interfaces
{
    public interface IMessagesService
    {
        Task<List<MessagesDTO>> GetMessagesList();
        Task Create(MessagesDTO item);
        Task Save();
    }
}
