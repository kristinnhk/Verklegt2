

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
    public class HomeController : StokerController
    {
        //add private member db context so we dont have to make a new one everytime
        //each service will have this. this is just here in controller for testing atm
        private ApplicationDbContext db = new ApplicationDbContext();
        private InterestService Is = new InterestService();
        private ThreadService ts = new ThreadService();
        private GroupService gs = new GroupService();
        public override ActionResult Index()
        {
            ViewModel model = new ViewModel();
            model.Users = new List<ApplicationUser>();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.threads = new List<ThreadModel>();

            var threads = ts.GetLatestThreadsAll();
            foreach (var thread in threads)
            {
                model.threads.Add(thread);
            }


            return View(model);
        }

				public ActionResult Test()
				{
					List<TemporaryThreadObjectModel> ThreadList = new List<TemporaryThreadObjectModel>();
					ThreadList.Add(new TemporaryThreadObjectModel() { 
						threadID = 1,
						user = User,
						title = "cute cats",
						interest = "Cats",
						mainContent = "lolololololol",
						comments = new List<CommentModel>() {
							new CommentModel() {
								commentID = 1,
								commentAuthor = "Skúli",
								content = "bad status bro",
								dateCreated = DateTime.Now,
								likes = 100,
								currentUserLiked = false
							},
							new CommentModel() {
								commentID = 2,
								commentAuthor = "Steinn",
								content = "yolo",
								dateCreated = DateTime.Now,
								likes = 0,
								currentUserLiked = false
							}
						},
						dateCreated = DateTime.Now,
						likes = 5,
						currentUserLiked = false
					});
					ThreadList.Add(new TemporaryThreadObjectModel()
					{
						threadID = 2,
						user = User,
						title = "CoolGoal",
						interest = "Football",
						mainContent = "RONALDOOOOOOO!!",
						comments = new List<CommentModel>() {
							new CommentModel() {
								commentID = 1,
								commentAuthor = "h.ki",
								content = "bbbbbbb",
								dateCreated = DateTime.Now,
								likes = 100,
								currentUserLiked = false
							},
							new CommentModel() {
								commentID = 2,
								commentAuthor = "Steinn",
								content = "yolo",
								dateCreated = DateTime.Now,
								likes = 0,
								currentUserLiked = false
							}
						},
						dateCreated = DateTime.Now,
						likes = 5,
						currentUserLiked = false
					});
					ViewBag.List = ThreadList;
					return View("NewsFeedPartial");
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