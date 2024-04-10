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
    public class MessagesService: IMessagesService
    {
        IUnitOfWork Database { get; set; }
        public MessagesService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<List<MessagesDTO>> GetMessagesList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Messages, MessagesDTO>().ForMember("User", opt => opt.MapFrom(c => c.User)));
            var mapper = new Mapper(config);
            return mapper.Map<List<Messages>, List<MessagesDTO>>(await Database.Messages.GetMessagesList());
        }
        public async Task Create(MessagesDTO item)
        {
            var message = new Messages
            {
                Id = item.Id,
                Message = item.Message,
                UserId = item.UserId,
                Date = item.Date
            };
            await Database.Messages.Create(message);
        }
        public async Task Save()
        {
            await Database.Save();
        }
    }
}
