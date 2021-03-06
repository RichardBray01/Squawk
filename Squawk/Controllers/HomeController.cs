﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Squawk.Models;

namespace Squawk.Controllers
{
    public class HomeController : Controller
    {

        private DatabaseContext db = new DatabaseContext();


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}