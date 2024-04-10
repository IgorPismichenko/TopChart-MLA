using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_DLL.Entities;

namespace TopChart_BLL.Interfaces
{
    public interface IUsersService
    {
        Task<List<UsersDTO>> GetUsersList();
        List<UsersDTO> CheckUser(LoginModelDTO logon);
        List<UsersDTO> CheckRegisterUser(RegisterModelDTO reg);
        UsersDTO GetUserById(int? Id);
        bool UserExists(int id);
        Task Create(UsersDTO item);
        Task Update(UsersDTO item);
        Task Delete(int id);
        Task Save();
    }
}
