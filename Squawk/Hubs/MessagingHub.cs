using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Squawk
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
            SendChartData();
        }

        void SendChartData()
        {
            Clients.All.asyncMessageReceiver();

        }
    }
}