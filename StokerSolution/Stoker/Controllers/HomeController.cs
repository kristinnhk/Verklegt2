

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
    [HandleError]
    public class HomeController : StokerController
    {
        /// <summary>
        /// The main frontpage, we instanciate the view model and 
        /// we get the necessary threads to display on the frontpage
        /// Then we send the view model to our view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //instanciating necessary lists
            ViewModel model = new ViewModel();
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
			model.sidebar = new SidebarModel();

            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            model.Users.Add(user);
            if (userID != null)
            {
                model.sidebar.userGroups = groupService.GetUserGroups(userID).ToList();
                model.sidebar.userInterests = interestService.GetUserInterests(userID).ToList();
                model.threads = threadService.GetFilteredThreads(userID).ToList();
            }
            // if any of the lists above are empty we instanciate them.
            if (model.sidebar.userGroups == null)
            {
                model.sidebar.userGroups = new List<GroupModel>();
            }
            if (model.sidebar.userInterests == null)
            {
                model.sidebar.userInterests = new List<InterestModel>();
            }
            if (model.threads == null)
            {
                model.threads = new List<ThreadModel>();
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