using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Stoker.Models;
using Stoker.Services;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Stoker.Controllers
{
    public class GroupSettingsController : StokerController
    {

        private static ApplicationDbContext db = new ApplicationDbContext();
        private GroupService groupService = new GroupService(db);

        public ActionResult GroupSettings(int groupID)
        {
            ViewModel model = new ViewModel();

            GroupModel group = groupService.GetGroupByID(groupID);

            model.groups = new List<GroupModel>();
            model.groups.Add(group);

            return View(model);
        }

        [HttpPost]
        public void UpdateAboutGroup()
        {
            string aboutGroupString = Request["aboutGroup"].ToString();
            int groupID = Convert.ToInt32(Request["groupID"]);
           groupService.SetGroupAbout(groupID, aboutGroupString);
        }

        [HttpPost]
        public ActionResult UpdateImage(FormCollection collection)
        {
            //  string file = collection["imgFileInUserSettings"];
            HttpPostedFileBase file = Request.Files[0];
            byte[] image = FileToByteArray(file);
            int thisGroupID = Convert.ToInt32(collection["hiddenGroupID"]);
            groupService.SetImage(thisGroupID, image);


            return RedirectToAction("GroupSettings", "GroupSettings", new { groupID = thisGroupID });
        }
	}
}