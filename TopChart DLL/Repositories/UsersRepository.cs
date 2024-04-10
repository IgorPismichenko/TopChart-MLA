using Microsoft.EntityFrameworkCore;
using TopChart_DLL.EF;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class UsersRepository: IRepositoryUsers
    {
        private readonly TopChartDbMLAContext _context;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public UsersRepository(TopChartDbMLAContext context)
        {
            _context = context;
        }
        public async Task<List<Users>> GetUsersList()
        {
            _semaphore.Wait();
            try
            {
                return await _context.Users.ToListAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public List<Users> CheckUser(LoginModel logon)
        {
            _semaphore.Wait();
            try
            {
                return _context.Users.Where(a => a.Login == logon.Login).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public List<Users> CheckRegisterUser(RegisterModel reg)
        {
            return _context.Users.Where(a => a.Login == reg.Login).ToList();
        }

        public Users GetUserById(int? Id)
        {
            return _context.Users.FirstOrDefault(a => a.Id == Id);
        }
        public async Task Create(Users u)
        {
            _semaphore.Wait();
            try
            {
                await _context.Users.AddAsync(u);
            }
            finally
            {
                _semaphore.Release();
            }
            _semaphore.Wait();
            try
            {
                await _context.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Update(Users u)
        {
            _context.Entry(u).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Users? u = await _context.Users.FindAsync(id);
            if (u != null)
                _context.Users.Remove(u);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
