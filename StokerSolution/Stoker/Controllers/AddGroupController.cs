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
    public class AddGroupController : StokerController
    {

        //
        // GET: /AddGroup/
        public ActionResult AddGroup()
        {
            return View();
        }

        public ActionResult Add(FormCollection collection)
        {
            GroupModel model = new GroupModel();

            model.title = collection["titleInGroup"];
            model.about = collection["aboutInGroup"];
            string userID = User.Identity.GetUserId();
            model.groupAdmin = userService.GetUserByID(userID);
            model.users = new List<ApplicationUser>();
            model.users.Add(model.groupAdmin);

            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength != 0)
            {
                model.image = FileToByteArray(file);
            }
            
            model.numberOfGroupMembers = 1;
            groupService.SetGroup(model);
            return RedirectToAction("GroupProfile", "GroupProfile", new { groupID =  model.groupID});
        }
	}
}