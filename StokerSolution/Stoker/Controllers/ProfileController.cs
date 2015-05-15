using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

using Microsoft.AspNet.Identity;

namespace Stoker.Controllers
{
    [HandleError]
    [Authorize]
    public class ProfileController : StokerController
    {
        [Authorize]
        public ActionResult Index()
        {
            return UserIndex(User.Identity.GetUserId());
        }
        [Authorize]
        public ActionResult FriendIndex(string userID)
        {
            return UserIndex(userID);
        }

        [Authorize]
        public ActionResult UserIndex(string userID)
        {
            ApplicationUser user = userService.GetUserByID(userID);
            ViewModel model = new ViewModel();
            //Initiating the parts of the view model needed. 
            model.Users = new List<ApplicationUser>();
            model.Users.Add(user);
            model.friendRequests = new List<ApplicationUser>();
			model.sidebar = new SidebarModel();
			model.sidebar.userGroups = new List<GroupModel>();
			model.sidebar.userInterests = new List<InterestModel>();
            //Getting the current users groups and interests
            ApplicationUser thisUser = userService.GetUserByID(User.Identity.GetUserId());
            if (thisUser.Id != null)
            {
                model.groups = GetUserGroups(thisUser.Id);
                model.interests = GetUserInterests(thisUser.Id);
                model.sidebar.userGroups = GetUserGroups(thisUser.Id);
                model.sidebar.userInterests = GetUserInterests(thisUser.Id);
            }
            else
            {
                model.groups = new List<GroupModel>();
                model.interests = new List<InterestModel>();
            }
            //Getting the threads on this users profile
            int userProfile = -1;
            model.threads = threadService.GetFilteredThreads(userID,0,0,userProfile).ToList();
            if (model.threads == null)
            {
                model.threads = new List<ThreadModel>();
            } 
            //Getting the users friend requests if this is the current users profile
            if (userID == thisUser.Id)
            {
                model.friendRequests = userService.GetFriendRequests(userID).ToList();
            }
            else if(userService.FriendRequestSent(thisUser.Id, userID) == false)
            {
                model.friendRequests = new List<ApplicationUser>();
                model.friendRequests.Add(user);
            }
            model.Users.Add(user);
            
            return View(model);
        }

        public List<GroupModel> GetUserGroups(string userID)
        {
            return groupService.GetUserGroups(userID).ToList();
        }

        public List<InterestModel> GetUserInterests(string userID)
        {
            return interestService.GetUserInterests(userID).ToList();
        }

        [HttpPost]
        public ActionResult SubmitUserThread(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();

            model = FillThreadModel(thread);

            string userID = User.Identity.GetUserId();
            threadService.SetUserThread(userID, model);
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult ChangeAboutMe()
        {
            return View("UserSettings", "UserSettings");
        }

        [HttpPost]
        public ActionResult AddFriend()
        {
            string thisUserID = User.Identity.GetUserId();
            string friendID = Request["userID"].ToString();
            userService.SendFriendRequest(thisUserID, friendID);
            return UserIndex(friendID);
        }
    }
}