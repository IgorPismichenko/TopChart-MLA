using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopChart_DLL.EF;
using TopChart_DLL.Entities;
using TopChart_DLL.Interfaces;

namespace TopChart_DLL.Repositories
{
    public class MessagesRepository:IRepositoryMessages
    {
        private readonly TopChartDbMLAContext _context;

        public MessagesRepository(TopChartDbMLAContext context)
        {
            _context = context;
        }

        public async Task<List<Messages>> GetMessagesList()
        {
            return await _context.Messages.Include(p => p.User).ToListAsync();
        }

        public async Task Create(Messages m)
        {
            await _context.Messages.AddAsync(m);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
