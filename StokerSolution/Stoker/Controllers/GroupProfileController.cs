using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stoker.Controllers
{
    public class GroupProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        private InterestService interestService = new InterestService();
        private ThreadService threadService = new ThreadService();
        // GET: GroupProfile
        public ActionResult GroupProfile(int groupId)
        {

            ViewModel model = new ViewModel();

            GroupModel group = db.groups.FirstOrDefault(x => x.groupID == groupId);

                model.Users = new List<ApplicationUser>();
                model.groups = new List<GroupModel>();
                model.interests = new List<InterestModel>();

                model.groups.Add(group); 
            
            return View(model);
        }
    }
}