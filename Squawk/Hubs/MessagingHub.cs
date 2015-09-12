using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Squawk.Models;


namespace Squawk
{
    public class ChatHub : Hub
    {
        private DatabaseContext db = new DatabaseContext();

        public void Send(string name, string message)
        {
            SendChartData();
        }

        void SendChartData()
        {
            Clients.All.asyncMessageReceiver(db.HistSamples);

        }
    }
}