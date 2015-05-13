using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;

using System.Web;
using System.Web.Mvc;

namespace Stoker.Controllers
{
    public class GroupProfileController : StokerController
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService(db);
        private GroupService groupService = new GroupService(db);
        private InterestService interestService = new InterestService(db);
        private ThreadService threadService = new ThreadService(db);
        // GET: GroupProfile
        public ActionResult GroupProfile(int groupId)
        {

            ViewModel model = new ViewModel();

            GroupModel group = db.groups.FirstOrDefault(x => x.groupID == groupId);

                model.Users = new List<ApplicationUser>();
                model.groups = new List<GroupModel>();
                model.interests = new List<InterestModel>();
								model.sidebar = new SidebarModel();
								model.sidebar.userGroups = new List<GroupModel>();
								model.sidebar.userInterests = new List<InterestModel>();

                model.groups.Add(group); 
            
            return View(model);
        }

        [HttpPost]
        public ActionResult SubmitGroupThread(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();
            int currentGroupID = Convert.ToInt32(thread["groupID"]);
            model = FillThreadModel(thread);
            string userID = User.Identity.GetUserId();
            threadService.SetGroupThread(currentGroupID, userID, model);

            return RedirectToAction("GroupProfile", "GroupProfile", new { groupID = currentGroupID });
        }

        public ActionResult GroupSettings(int goToGroupID)
        {
            return RedirectToAction("GroupSettings", "GroupSettings", new { groupID = goToGroupID });
        }
    }
}