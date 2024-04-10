using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{
    public interface IRepositoryMessages
    {
        Task<List<Messages>> GetMessagesList();
        Task Create(Messages item);
        Task Save();
    }
}
