﻿using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace Stoker.Controllers
{
    [HandleError]
    [Authorize]
    public class InterestController : StokerController
    {
        public ActionResult Interest(int interestId)
        {
            ViewModel model = new ViewModel();
            string userID = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userID);

            InterestModel interest = db.interests.FirstOrDefault(x => x.interestID == interestId);

            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.sidebar = new SidebarModel();
            model.sidebar.userGroups = new List<GroupModel>();
            model.sidebar.userInterests = new List<InterestModel>();
            int interestProfile = 2;
            model.threads = threadService.GetFilteredThreads(userID, 0, 0, interestProfile).ToList();

            if (user.Id != null)
            {
                var groups = groupService.GetUserGroups(User.Identity.GetUserId());
                foreach (GroupModel group in groups)
                {
                    model.sidebar.userGroups.Add(group);
                }
            }

            var interests = interestService.GetUserInterests(User.Identity.GetUserId());
            foreach (InterestModel i in interests)
            {            
                model.sidebar.userInterests.Add(i);
            }
            model.interests.Add(interest);
            return View(model);
        }

        public void FollowInterest()
        {
            int id = Convert.ToInt32(Request["id"]);
            interestService.SetUserInterest(id, User.Identity.GetUserId());
        }

        public void UnFollowInterest()
        {
            int id = Convert.ToInt32(Request["id"]);
            interestService.DeleteUserInterest(User.Identity.GetUserId(), id);
        }

        public ActionResult SubmitInterestThread(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();

            model = FillThreadModel(thread);
            int currentInterestID = Convert.ToInt32(thread["interestID"]);
            string userID = User.Identity.GetUserId();
            threadService.SetInterestThread(currentInterestID,userID, model);
            return RedirectToAction("Interest", "Interest", new { interestId = currentInterestID});
        }

        public bool IsFollowing()
        {
            string userID = User.Identity.GetUserId();
            int interestID = Convert.ToInt32(Request["interestID"]);
            bool result = interestService.IsFollowingInterest(userID, interestID);
            return result;
        }

    }
}