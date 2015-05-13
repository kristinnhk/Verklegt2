﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Stoker.Services;  
using Stoker.Models;


namespace Stoker.Controllers
{
    [Authorize]
    public class HomeController : StokerController
    {
        //add private member db context so we dont have to make a new one everytime
        //each service will have this. this is just here in controller for testing atm

        public ActionResult Index()
        {
            ViewModel model = new ViewModel();
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.threads = new List<ThreadModel>();
						model.sidebar = new SidebarModel();
						model.sidebar.userInterests = new List<InterestModel>();
						model.sidebar.userGroups = new List<GroupModel>();

            var threads = threadService.GetFrontPageThreads();
            foreach (var thread in threads)
            {
                model.threads.Add(thread);
            }


            return View(model);
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