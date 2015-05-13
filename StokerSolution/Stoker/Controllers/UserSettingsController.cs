using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Stoker.Models;
using Stoker.Services;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Stoker.Controllers
{
    public class UserSettingsController : StokerController
    {
        //
        // GET: /UserSettings/
        public ActionResult UserSettings()
        {
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            ViewModel model = new ViewModel();

            GroupModel group22 = new GroupModel();

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

            if (user.image == null)
            {
                userService.SetImageDefault(user.Id);
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

        [HttpPost]
        public ActionResult UpdateImage(FormCollection collection)
        {
          //  string file = collection["imgFileInUserSettings"];
            HttpPostedFileBase file = Request.Files[0];
            byte[] image = FileToByteArray(file);
            userService.SetImage(User.Identity.GetUserId(), image);

            return RedirectToAction("UserSettings","UserSettings");
        }

        public ActionResult AddGroup()
        {
            return RedirectToAction("AddGroup", "AddGroup");
        }

	}
}
