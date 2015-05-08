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
        //
        // GET: /UserSettings/
        public ActionResult UserSettings()
        {
            UserService userService = new UserService();
            GroupService groupService = new GroupService();
            string tempid = User.Identity.GetUserId();
            //service.SetAboutMe(tempid, "Steinnvaradbreyta");
            //ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == tempid);
            //union.User = db.Users.FirstOrDefault(x => x.Id == tempid);
            //ApplicationUser model = service.GetUserByID("c14bc092-cbe2-418e-ba36-e86021da5a05");

            //creating a new group
            GroupModel newGroup = new GroupModel();
            newGroup.title = "Bolti a thridjudogum";
            newGroup.about = "I KR heimilinu kl. 20:00";
            newGroup.numberOfGroupMembers = 2;
            groupService.SetGroup(newGroup);

            return View();
        }
        

	}
}
