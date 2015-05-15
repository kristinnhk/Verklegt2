using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Stoker.Models;

namespace Stoker.Controllers
{
    [HandleError]
    [Authorize]
    public class ThreadDetailController : StokerController
    {
        // GET: ThreadDetail
        public ActionResult ThreadDetail(int threadID)
        {
            ViewModel model = new ViewModel();
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.threads = new List<ThreadModel>();
            model.sidebar = new SidebarModel();
            model.sidebar.userGroups = new List<GroupModel>();
            model.sidebar.userInterests = new List<InterestModel>();
            ApplicationUser thisUser = userService.GetUserByID(User.Identity.GetUserId());
            string userID = thisUser.Id;
            if (thisUser.Id != null)
            {
                model.sidebar.userGroups = groupService.GetUserGroups(userID).ToList();
                model.sidebar.userInterests = interestService.GetUserInterests(userID).ToList();
            }
            ThreadModel thread = threadService.GetThreadByID(threadID);
            thread.comments = thread.comments.OrderByDescending(CommentModel => CommentModel.dateCreated).ToList();
            model.threads.Add(thread);
            return View(model);
        }

        public ActionResult SubmitThreadComment(FormCollection collection)
        {
            CommentModel comment = new CommentModel();
            comment.content = collection["commentContent"];
            comment.dateCreated = DateTime.Now;
            comment.likes = 0;
         //   comment.usersLiked = new List<ApplicationUser>();
            string userID = User.Identity.GetUserId();
          
            int id = Convert.ToInt32(collection["threadID"]);
          //  comment.thread = threadService.GetThreadByID(id);

            threadService.SetThreadComment(id, userID, comment);

            return RedirectToAction("ThreadDetail", "ThreadDetail", new { threadID = id });
        }
    }
}