using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentMe.Hubs
{
    public class MessageHub : Hub
    {
        public static readonly Dictionary<string, List<string>> _userConnections = new Dictionary<string, List<string>>();
        private readonly object connectionLock = new object();

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();
            var connectionId = Context.ConnectionId;

            lock (connectionLock)
            {
                if (_userConnections.ContainsKey(userId))
                {
                    _userConnections[userId].Add(connectionId);
                }
                else
                {
                    _userConnections.Add(userId, new List<string> { connectionId });
                }
            }


            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            _userConnections[userId].Remove(connectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
