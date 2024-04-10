using Microsoft.AspNetCore.SignalR;
using TopChart_BLL.DTO;
using TopChart_BLL.Interfaces;
using TopChart_DLL.Entities;

namespace TopChart
{
    public class ChatHub : Hub
    {
        static List<UsersDTO> Users = new List<UsersDTO>();
        IUsersService repoUsers;
        IMessagesService repoMess;

        public ChatHub(IUsersService u, IMessagesService m)
        {
            repoUsers = u;
            repoMess = m;
        }
        public async Task Send(string username, string message)
        {
            MessagesDTO mess = new MessagesDTO();
            mess.Message = message;
            mess.Date = DateTime.Now.ToString();
            var users = await repoUsers.GetUsersList();
            foreach (var user in users)
            {
                if (user.Login == username)
                {
                    mess.UserId = user.Id;
                }
            }
            repoMess.Create(mess);
            repoMess.Save();
            await Clients.All.SendAsync("AddMessage", username, message);
        }

        public async Task Connect(string userName)
        {
            var id = Context.ConnectionId;
            var users = await repoUsers.GetUsersList();
            var newUser = new UsersDTO();
            foreach (var user in users)
            {
                if (user.Login == userName)
                {
                    user.ConnectionId = id;
                    newUser = user;
                    //await repoUsers.Update(user);
                    //await repoUsers.Save();
                }
            }
            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(newUser);
                await Clients.Caller.SendAsync("Connected", id, userName, Users);
                await Clients.AllExcept(id).SendAsync("NewUserConnected", id, userName);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                await Clients.All.SendAsync("UserDisconnected", id, item.Login);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
