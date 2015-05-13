using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Stoker.Models;

namespace Stoker.Controllers
{
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

            ThreadModel thread = threadService.GetThreadByID(threadID);
            model.threads.Add(thread);

            return View(model);
        }

        public ActionResult SubmitThreadComment(FormCollection collection)
        {

            return View();
        }
    }
}