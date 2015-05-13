﻿using Stoker.Models;
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
using System.Drawing;

namespace Stoker.Controllers
{
    public class ProfileController : StokerController
    {

        // GET: Profile
        public override ActionResult Index()
        {
            return UserIndex(User.Identity.GetUserId());
        }

        public ActionResult FriendIndex(string userID)
        {
            return UserIndex(userID);
        }

        [Authorize]
        public ActionResult UserIndex(string userID)
        {
            ApplicationUser user = userService.GetUserByID(userID);
            //userService.SendFriendRequest("31307c68-78d1-4749-8bb4-5a402ba4646d", userID);
            ViewModel model = new ViewModel();
            //Initiating the parts of the view model needed. 
            model.Users = new List<ApplicationUser>();
            model.Users.Add(user);
			model.sidebar = new SidebarModel();
			model.sidebar.userGroups = new List<GroupModel>();
			model.sidebar.userInterests = new List<InterestModel>();
            //Getting the current users groups and interests
            ApplicationUser thisUser = userService.GetUserByID(User.Identity.GetUserId());
            if (thisUser.Id != null)
            {
                model.groups = GetUserGroups(thisUser.Id);
                model.interests = GetUserInterests(thisUser.Id);
            }
            else
            {
                model.groups = new List<GroupModel>();
                model.interests = new List<InterestModel>();
            }
            //Getting the threads on this users profile
            model.threads = threadService.GetUserThreads(userID).ToList();
            if (model.threads == null)
            {
                model.threads = new List<ThreadModel>();
            }
            //Getting the users friend requests if this is the current users profile
            if (userID == thisUser.Id)
            {
                model.friendRequests = userService.GetFriendRequests(userID).ToList();
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