using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
namespace SignalRChat
{

    public class ChatHub : Hub
    {
        public static List<UserInfo> OnlineUsers =  new List<UserInfo>();

        public void Send(string name, string message)
        {
            var connectionID = "";

            if (name == "Albert")
            {
                connectionID = OnlineUsers.Find(x => x.UserName == "Joey").UserConnectionID.ToString();
                Clients.Client(connectionID).addNewMessageToPage(name, message);
            }

            else if (name == "Joey")
            {
                connectionID = OnlineUsers.Find(x => x.UserName == "Albert").UserConnectionID.ToString();
                Clients.Client(connectionID).addNewMessageToPage(name, message);
            }

            else
                Clients.All.addNewMessageToPage(name, message);
        }
        public void connect(string username)
        {
            var connectionID = Context.ConnectionId;
            // Call the addNewMessageToPage method to update clients.
            OnlineUsers.Add(new UserInfo { UserName = username, UserID = Guid.NewGuid().ToString(), UserConnectionID = connectionID });
        }
    }

    public class UserInfo
    {
        public string UserConnectionID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}