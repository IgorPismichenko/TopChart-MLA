using TopChart_DLL.Entities;

namespace TopChart_DLL.Interfaces
{ 
    public interface IRepositoryUsers
    {
        Task<List<Users>> GetUsersList();
        List<Users> CheckUser(LoginModel logon);
        List<Users> CheckRegisterUser(RegisterModel reg);
        Users GetUserById(int? Id);
        bool UserExists(int id);
        Task Create(Users item);
        void Update(Users item);
        Task Delete(int id);
        Task Save();
    }
}
