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
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        private InterestService interestService = new InterestService();
        private ThreadService threadService = new ThreadService();
        
        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            
            ViewModel model = new ViewModel();
            //Initiating the parts of the view model needed. 
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();

            if (user.Id != null)
            {
                var groups = GetUserGroups(user.Id);
                foreach (GroupModel group in groups)
                {
                    model.groups.Add(group);
                }
            }

            var interests = GetUserInterests(user.Id);
            foreach (InterestModel interest in interests)
            {
                model.interests.Add(interest);
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
        public ActionResult SubmitThread(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();
            string title = Convert.ToString(thread["titleInUserThread"]);
            //Image image = thread["imageInUserThread"];
            string content = Convert.ToString(thread["contentInUserThread"]);
            model.title = title;
            model.mainContent = content;
            model.dateCreated = DateTime.Now;
            //model.image = image;
            model.likes = 0;
            model.currentUserLiked = false;
            
            threadService.SetUserThread(User.Identity.GetUserId(), model);
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult ChangeAboutMe()
        {
            return View("UserSettings", "UserSettings");
        }
    }
}