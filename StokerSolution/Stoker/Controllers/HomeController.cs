using Stoker.Models;
using Stoker.Models.UnionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Stoker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
         /*   UserInterestUnion test = new UserInterestUnion();

            InterestModel interest = new InterestModel();
            interest.name = "cats";
            interest.numberOfUsersInterested = 10;
            db.interests.Add(interest);
            db.SaveChanges();
            test.interest = interest;
            ApplicationUser user = (from s in db.Users
                                    select s).First();
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
              //  db.SaveChanges();
            }
            test.interest = (from i in db.interests
                             select i).First();

            testfunction(test);*/
            return View();
        }
        [HttpPost]
        public ActionResult testfunction(UserInterestUnion test)
        {

            if (ModelState.IsValid)
            {
                db.userInterestUnion.Add(test);
            //    db.SaveChanges();
            }

            //var temp = User.Identity.GetUserId();
            // test.UserId = User.Identity.GetUserId();

            // AddUserFriend(, int userID2)
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}