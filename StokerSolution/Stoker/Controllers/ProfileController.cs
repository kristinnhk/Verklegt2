using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using System.Drawing;

namespace Stoker.Controllers
{
    public class ProfileController : StokerController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        private InterestService interestService = new InterestService();
        private ThreadService threadService = new ThreadService();
        
        // GET: Profile
        [Authorize]
        public override ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            
            ViewModel model = new ViewModel();
            //Initiating the parts of the view model needed. 
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
						model.sidebar = new SidebarModel();
						model.sidebar.userGroups = new List<GroupModel>();
						model.sidebar.userInterests = new List<InterestModel>();
					
						groupService.SetUserGroup(User.Identity.GetUserId(), 13);
						groupService.SetUserGroup(User.Identity.GetUserId(), 16);
						groupService.SetUserGroup(User.Identity.GetUserId(), 18);
						groupService.SetUserGroup(User.Identity.GetUserId(), 20);
            if (user.Id != null)
            {
                var groups = GetUserGroups(user.Id);
                foreach (GroupModel group in groups)
                {
                    model.groups.Add(group);
										model.sidebar.userGroups.Add(group);
                }
            }

            var interests = GetUserInterests(user.Id);
            foreach (InterestModel interest in interests)
            {
                model.interests.Add(interest);
								model.sidebar.userInterests.Add(interest);
            }

            model.Users.Add(user);

            return View(model);
        }
        public List<GroupModel> GetUserGroups(string userID)
        {
            return groupService.GetUserGroups(userID).ToList();
        }

        public List<InterestModel> GetUserInterests(string userID)
        {
            return interestService.GetUserInterests(userID).ToList();
        }
        [HttpPost]
        public ActionResult SubmitUserThread(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();

            model = FillThreadModel(thread);
            
            
            threadService.SetUserThread(User.Identity.GetUserId(), model);
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult ChangeAboutMe()
        {
            return View("UserSettings", "UserSettings");
        }
    }
}