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
    public class AddGroupController : Controller
    {
        GroupService service = new GroupService();
        //
        // GET: /AddGroup/
        public ActionResult AddGroup()
        {
            return View();
        }

        public ActionResult Add(GroupModel model)
        {
            model.numberOfGroupMembers = 1;
            service.SetGroup(model);
            return RedirectToAction("GroupProfile", "GroupProfile", new { groupID =  model.groupID});
        }
	}
}