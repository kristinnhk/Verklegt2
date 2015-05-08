using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Stoker.Services;  


using Stoker.Models;
using Stoker.Models.UnionModels;

namespace Stoker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //add private member db context so we dont have to make a new one everytime
        //each service will have this. this is just here in controller for testing atm
        private ApplicationDbContext db = new ApplicationDbContext();
        private InterestService Is = new InterestService();

        public ActionResult Index()
        {
            //create a new union model
         //   UserInterestUnion union = new UserInterestUnion();

            //adding an interest to the database
            //do not need to specify the primary key
            
           /* InterestModel interest = new InterestModel();
            interest.name = "cats";
            interest.numberOfUsersInterested = 10;*/

            //if you do not comment out these 2 lines after adding the interest the frist time
            //then you will get alot of the same interest since the database doesnt check if 
            //name is unique
          //  db.interests.Add(interest);
          //  db.SaveChanges();
/*
            int interestidtotest = 1;
            Is.SetInterestTitle(interestidtotest, "dogs");
            InterestModel test = Is.GetInterestByID(interestidtotest);
            int good = 0;
/*
            if (test.name == "dogs")
            {

                good++;
            }
            else
            {
                good--;
            }
            */

            //to put a union in the database we have to give the function the two models we want to link together
            //it is not enough to give it the primary key of the models, we have to give it the models themselves
            //so it can link them together

            //two ways to get a user, the first one gets a user from the db
            //the other gets the current user id and then searches for him the the db using lambda expr.
        //this : 
         //   union.User = (from s in db.Users
          //                select s).First();
           //or this : 
     /*       string tempid = User.Identity.GetUserId();
            union.User = db.Users.FirstOrDefault(x => x.Id == tempid);
            //
            union.interestID = (from i in db.interests
                                select i).First();
            //adding to the database
            db.userInterestUnion.Add(union);
            db.SaveChanges();
            IEnumerable<ApplicationUser> list = Is.GetInterestUsers(1);*/

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