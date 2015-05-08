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
            UserService service = new UserService();
            string tempid = User.Identity.GetUserId();
            service.ChangeAboutMe(tempid, "Steinnvaradbreyta");
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == tempid);
            //union.User = db.Users.FirstOrDefault(x => x.Id == tempid);
            //ApplicationUser model = service.GetUserByID("c14bc092-cbe2-418e-ba36-e86021da5a05");
            return View(user);
        }
        

	}
}