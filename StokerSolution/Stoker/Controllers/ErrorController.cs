using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

using Microsoft.AspNet.Identity;

namespace Stoker.Controllers
{
    public class ErrorController : StokerController
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser thisUser = userService.GetUserByID(userID);
            ViewModel model = new ViewModel();
            //Initiating the parts of the view model needed. 
            model.Users = new List<ApplicationUser>();
            model.Users.Add(thisUser);
            model.friendRequests = new List<ApplicationUser>();
            model.sidebar = new SidebarModel();
            model.sidebar.userGroups = new List<GroupModel>();
            model.sidebar.userInterests = new List<InterestModel>();
            model.groups = GetUserGroups(userID);
            model.interests = GetUserInterests(userID);
            model.friendRequests = new List<ApplicationUser>();
            model.Users.Add(thisUser);

            return View("Error", model);
        }

        public List<GroupModel> GetUserGroups(string userID)
        {
            return groupService.GetUserGroups(userID).ToList();
        }

        public List<InterestModel> GetUserInterests(string userID)
        {
            return interestService.GetUserInterests(userID).ToList();
        }
	}
}