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
        //
        // GET: /UserSettings/
        public ActionResult UserSettings()
        {
            string userID = User.Identity.GetUserId();
            //service.SetAboutMe(tempid, "Steinnvaradbreyta");
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);
            //union.User = db.Users.FirstOrDefault(x => x.Id == tempid);
            //ApplicationUser model = service.GetUserByID("c14bc092-cbe2-418e-ba36-e86021da5a05");

            //creating a new group
            //GroupModel newGroup = new GroupModel();
            //newGroup.title = "Bolti a thridjudogum";
            //newGroup.about = "I KR heimilinu kl. 20:00";
            //newGroup.numberOfGroupMembers = 2;
            //groupService.SetGroup(newGroup);

            return View(user);
        }
        
        /// <summary>
        /// takes in an ajax request with with a string containing updated
        /// information for the about me field of the user and updates in the database.
        /// </summary>
        public void UpdateAboutMe()
        {
            string aboutMeString = Request["aboutMe"].ToString();
            string userID = User.Identity.GetUserId();
            userService.SetAboutMe(userID, aboutMeString);
        }

	}
}
