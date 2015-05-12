using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Stoker.Services;  
using Stoker.Models;


namespace Stoker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //add private member db context so we dont have to make a new one everytime
        //each service will have this. this is just here in controller for testing atm
        private ApplicationDbContext db = new ApplicationDbContext();
        private InterestService Is = new InterestService();
        private ThreadService ts = new ThreadService();
        private GroupService gs = new GroupService();
        public ActionResult Index()
        {
            //create a new union model
         //   UserInterestUnion union = new UserInterestUnion();

            //adding an interest to the database
            //do not need to specify the primary key
           /* CommentModel comment = new CommentModel();
            comment.content = "Such WoW";
            comment.dateCreated = DateTime.Now;
            comment.likes = 100;

            CommentModel comment2 = new CommentModel();
            comment2.content = "such quality";
            comment2.dateCreated = DateTime.Now;
            comment2.likes = 144;

            CommentModel comment3 = new CommentModel();
            comment3.content = "very nice";
            comment3.dateCreated = DateTime.Now;
            comment3.likes = 1337;

            ts.SetThreadComment(4, User.Identity.GetUserId(), comment);
            ts.SetThreadComment(4, User.Identity.GetUserId(), comment2);
            ts.SetThreadComment(4, User.Identity.GetUserId(), comment3);
            */
   /*         IEnumerable<CommentModel> comments = ts.GetThreadComments(4);
            foreach (var item in comments)
            {
                System.Diagnostics.Debug.WriteLine(item.content + ".  NUMBER OF LIKES: " + item.likes
                    + "\n DATE POSTED: " + item.dateCreated);
            }*/

            //if you do not comment out these 2 lines after adding the interest the frist time
            //then you will get alot of the same interest since the database doesnt check if 
            //name is unique
          /*  Is.SetNewInterest("dog pictures");
            Is.SetNewInterest("dog videos");
            Is.SetNewInterest("funny dogs");
            Is.SetNewInterest("stupid cats doing stupid stuff");
            Is.SetNewInterest("hoskuldur");*/

        /*    IEnumerable<InterestModel> test = Is.GetInterestsByTitle("dog");

            foreach (var item in test)
            {
                System.Diagnostics.Debug.WriteLine(item.name);
            }*/
      

       //     Is.SetUserInterest(2, User.Identity.GetUserId());
  
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
     //       int stopper = 5;
            
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