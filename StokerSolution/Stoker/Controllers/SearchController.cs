using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stoker.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Search(string query)
        {
            UserService userService = new UserService();
            ThreadService threadService = new ThreadService();
            InterestService interestService = new InterestService();
            GroupService groupService = new GroupService();

            ViewModel results = new ViewModel();

            List<ApplicationUser> users = userService.GetUsersByName("S").ToList();
            List<GroupModel> groups = groupService.GetGroupByTitle("Steinn").ToList();
            //List<InterestModel> interests = interestService.GetInterestsByTitle(query).ToList();
            //List<ThreadModel> threads = threadService.GetThreadByTitle(query).ToList();
            //results.interests = interests;
            //results.threads = threads;
            results.groups = groups;
            results.Users = users;

            return View(results);
        }
    }
}