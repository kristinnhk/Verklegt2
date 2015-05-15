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
    [HandleError]
    [Authorize]
    public class GroupProfileController : StokerController
    {
        public ActionResult GroupProfile(int groupID)
        {
            ViewModel model = new ViewModel();
            string userID = User.Identity.GetUserId();
            GroupModel group = groupService.GetGroupByID(groupID);
            ApplicationUser user = userService.GetUserByID(userID);
            model.Users = new List<ApplicationUser>();
            model.Users.Add(user);
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
			model.sidebar = new SidebarModel();
			model.sidebar.userGroups = new List<GroupModel>();
			model.sidebar.userInterests = new List<InterestModel>();
            int groupProfile = 1;
            model.threads = threadService.GetFilteredThreads(userID, 0, 0, groupProfile).ToList();
            if (user.Id != null)
            {
                var groups = groupService.GetUserGroups(User.Identity.GetUserId());
                foreach (GroupModel g in groups)
                {
                    model.sidebar.userGroups.Add(g);
                }
            }

            var interests = interestService.GetUserInterests(User.Identity.GetUserId());
            foreach (InterestModel i in interests)
            {
                model.sidebar.userInterests.Add(i);
            }

            model.groups.Add(group);

            if (groupService.IsMemberOfGroup(userID, groupID) == true)
            {
                return View(model);
            }
            else
            {
                return View("GroupNonMemberProfile", model);
            }
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

        public bool IsGroupMember()
        {
            string userID = User.Identity.GetUserId();
            int groupID = Convert.ToInt32(Request["groupID"]);
            bool result = groupService.IsMemberOfGroup(userID, groupID);
            return result;
        }

        public void LeaveGroup()
        {
            string userID = User.Identity.GetUserId();
            int groupID = Convert.ToInt32(Request["groupID"]);
            groupService.DeleteUserGroup(userID, groupID);
        }

        public void JoinGroup()
        {
            string userID = User.Identity.GetUserId();
            int groupID = Convert.ToInt32(Request["groupID"]);
            groupService.SetUserGroup(userID, groupID);
        }
    }
}