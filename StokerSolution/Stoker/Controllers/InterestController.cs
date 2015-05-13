using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace Stoker.Controllers
{
    public class InterestController : StokerController
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService(db);
        private GroupService groupService = new GroupService(db);
        private InterestService interestService = new InterestService(db);
        private ThreadService threadService = new ThreadService(db);
        // GET: Interest
        public ActionResult Interest(int interestId)
        {
            ViewModel model = new ViewModel();
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);

            InterestModel interest = db.interests.FirstOrDefault(x => x.interestID == interestId);


            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.sidebar = new SidebarModel();
            model.sidebar.userGroups = new List<GroupModel>();
            model.sidebar.userInterests = new List<InterestModel>();


            if (user.Id != null)
            {
                var groups = groupService.GetUserGroups(userID);
                foreach (GroupModel group in groups)
                {
                    model.sidebar.userGroups.Add(group);
                }
            }

            var interests = interestService.GetUserInterests(userID);
            foreach (InterestModel i in interests)
            {            
                model.sidebar.userInterests.Add(i);
            }

            model.interests.Add(interest);
            return View(model);
        }
        public void FollowInterest()
        {
            int id = Convert.ToInt32(Request["id"]);
            interestService.SetUserInterest(id, User.Identity.GetUserId());
        }

        public void UnFollowInterest()
        {

        }
    }
}