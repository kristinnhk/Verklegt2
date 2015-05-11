using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Stoker.Models;
using Stoker.Services;

namespace Stoker.Controllers
{
    public class UserSettingsController : StokerController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        private InterestService interestService = new InterestService();
        //
        // GET: /UserSettings/
        public ActionResult UserSettings()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            ViewModel model = new ViewModel();

            //groupService.SetUserGroup(User.Identity.GetUserId(), 1);
            //groupService.SetUserGroup(User.Identity.GetUserId(), 2);
            //groupService.SetUserGroup(User.Identity.GetUserId(), 3);
            //groupService.SetUserGroup(User.Identity.GetUserId(), 4);

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
            foreach(InterestModel interest in interests)
            {
                model.interests.Add(interest);
            }

            model.Users.Add(user);


            return View(model);
        }
        
        /// <summary>
        /// takes in an ajax request with with a string containing updated
        /// information for the about me field of the user and updates in the database.
        /// </summary>
        [HttpPost]
        public void UpdateAboutMe()
        {
            string aboutMeString = Request["aboutMe"].ToString();
            string userID = User.Identity.GetUserId();
            userService.SetAboutMe(userID, aboutMeString);
        }

        public List<GroupModel> GetUserGroups(string userID) 
        {
            return groupService.GetUserGroups(userID).ToList();
        }

        public List<InterestModel> GetUserInterests(string userID)
        {
            return interestService.GetUserInterests(userID).ToList();
        }

        public void DeleteUserGroups()
        {
            foreach (int ID in Request["groupIds[]"])
            {
               // groupService.DeleteUserGroup(User.Identity.GetUserId(), ID);
            }
        }

        public void DeleteUserInterests()
        {
            foreach (int ID in Request["interestIds[]"])
            {
                //   interestService.DeleteUserInterest(User.Identity.GetUserId(), ID);
            }
        }
	}
}
