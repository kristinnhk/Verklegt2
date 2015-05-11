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
        private UserService userService = new UserService();
        private ThreadService threadService = new ThreadService();
        private InterestService interestService = new InterestService();
        private GroupService groupService = new GroupService();

        // GET: Search
        public ActionResult Search()
        {
            return View();
        }
        /// <summary>
        /// This function uses a ajax script in the SearchJS.js file to search for all
        /// objects in the database with a recieved value. It does this by taking the value
        /// from a textfield, sending it to this function through an ajax post method and
        /// then returning a json object that appends the values to a table in Search.cshtml.
        /// </summary>
        /// <returns>Function returns a json object that includes everything thatsatisfies the query</returns>
        public ActionResult SearchJson()
        {
            ViewModel results = new ViewModel();
            string query = Request["Search"].ToString();
            List<ApplicationUser> users = userService.GetUsersByName(query).ToList();
            List<GroupModel> groups = groupService.GetGroupByTitle(query).ToList();
            List<InterestModel> interests = interestService.GetInterestsByTitle(query).ToList();
            //List<ThreadModel> threads = threadService.GetThreadByTitle(query).ToList();
            results.interests = interests;
            //results.threads = threads;
            results.groups = groups;
            results.Users = users;

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}