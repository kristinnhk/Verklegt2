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
    public class GroupSettingsController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        private GroupService groupService = new GroupService();

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
	}
}