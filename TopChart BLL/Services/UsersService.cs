using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_BLL.DTO;
using TopChart_BLL.Infrastructure;
using TopChart_BLL.Interfaces;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_BLL.Services
{
    public class UsersService:IUsersService
    {
        IUnitOfWork Database { get; set; }
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        public UsersService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<UsersDTO>> GetUsersList()
        {
            _semaphore.Wait();
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UsersDTO>()).CreateMapper();
                return mapper.Map<List<Users>, List<UsersDTO>>(await Database.Users.GetUsersList());
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public List<UsersDTO> CheckUser(LoginModelDTO logon)
        {
            _semaphore.Wait();
            try
            {
                var login = new LoginModel
                {
                    Login = logon.Login,
                    Password = logon.Password
                };
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UsersDTO>()).CreateMapper();
                return mapper.Map<List<Users>, List<UsersDTO>>(Database.Users.CheckUser(login));
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public List<UsersDTO> CheckRegisterUser(RegisterModelDTO reg)
        {
            var register = new RegisterModel
            {
                Login = reg.Login,
                Password = reg.Password,
                PasswordConfirm = reg.PasswordConfirm
            };
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Users, UsersDTO>()).CreateMapper();
            return mapper.Map<List<Users>, List<UsersDTO>>(Database.Users.CheckRegisterUser(register));
        }
        public UsersDTO GetUserById(int? Id)
        {
            var item = Database.Users.GetUserById(Id);
            if (item == null)
                throw new ValidationException("Wrong track!", "");
            return new UsersDTO
            {
               Id = item.Id,
               Login = item.Login,
               Password = item.Password,
               Salt = item.Salt,
               Status = item.Status
            };
        }
        public bool UserExists(int id)
        {
            var user = Database.Users.GetUserById(id);
            return user != null;
        }

        public async Task Create(UsersDTO item)
        {
            var user = new Users
            {
                Id = item.Id,
                Login = item.Login,
                Password = item.Password,
                Salt = item.Salt,
                Status = item.Status
            };
            await Database.Users.Create(user);
        }
        public void Update(UsersDTO item)
        {
            var user = new Users
            {
                Id = item.Id,
                Login = item.Login,
                Password = item.Password,
                Salt = item.Salt,
                Status = item.Status
            };
            Database.Users.Update(user);
        }
        public async Task Delete(int id)
        {
            await Database.Users.Delete(id);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
