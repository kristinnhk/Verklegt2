using Stoker.Models;
using Stoker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stoker.Controllers
{
    public class SearchController : StokerController
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
        /// Tvo ViewModels are used so as to not send to much data.
        /// </summary>
        /// <returns>Function returns a json object that includes everything thatsatisfies the query</returns>
        public ActionResult SearchJson()
       {
            ViewModel results = new ViewModel();
            ViewModel realResult = new ViewModel();
            realResult.Users = new List<ApplicationUser>();
            realResult.groups = new List<GroupModel>();
            realResult.interests = new List<InterestModel>();
            if (Request["Search"] != null)
            {
                string query = Request["Search"].ToString();

                List<ApplicationUser> users = userService.GetUsersByName(query).ToList();
                List<GroupModel> groups = groupService.GetGroupByTitle(query).ToList();
                List<InterestModel> interests = interestService.GetInterestsByTitle(query).ToList();
                results.interests = interests;
                results.groups = groups;
                results.Users = users;
                for(int i = 0; i < results.Users.Count; i++)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.firstName = results.Users[i].firstName;
                    user.lastName = results.Users[i].lastName;
                    user.Id = results.Users[i].Id;
                    realResult.Users.Add(user);
                }
                for(int i = 0; i < results.groups.Count; i++)
                {
                    GroupModel group = new GroupModel();
                    group.title = results.groups[i].title;
                    group.numberOfGroupMembers = results.groups[i].numberOfGroupMembers;
                    group.groupID = results.groups[i].groupID;
                    realResult.groups.Add(group);
                }
                for (int i = 0; i < results.interests.Count; i++)
                {
                    InterestModel interest = new InterestModel();
                    interest.name = results.interests[i].name;
                    interest.numberOfUsersInterested = results.interests[i].numberOfUsersInterested;
                    interest.interestID = results.interests[i].interestID;
                    realResult.interests.Add(interest);
                }
                return Json(realResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Search", "Search");
            }

            
        }
    }
}