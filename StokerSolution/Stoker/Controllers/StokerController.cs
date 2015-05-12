using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Stoker.Models;
using Stoker.Models.UnionModels;

namespace Stoker.Controllers
{
    public class StokerController : Controller
    {
        //
        // GET: /Stoker/
        public ActionResult Index()
        {
            return View();
        }

				//public ActionResult Test()
				//{
				//	List<ThreadModel> ThreadList = new List<ThreadModel>();
				//	ThreadList.Add(new ThreadModel()
				//	{
				//		threadID = 1,
				//		user = User,
				//		title = "cute cats",
				//		interest = "Cats",
				//		mainContent = "lolololololol",
				//		comments = new List<CommentModel>() {
				//			new CommentModel() {
				//				commentID = 1,
				//				commentAuthor = "Skúli",
				//				content = "bad status bro",
				//				dateCreated = DateTime.Now,
				//				likes = 100,
				//				currentUserLiked = false
				//			},
				//			new CommentModel() {
				//				commentID = 2,
				//				commentAuthor = "Steinn",
				//				content = "yolo",
				//				dateCreated = DateTime.Now,
				//				likes = 0,
				//				currentUserLiked = false
				//			}
				//		},
				//		dateCreated = DateTime.Now,
				//		likes = 5,
				//		currentUserLiked = false
				//	});
				//	ViewBag.List = ThreadList;
				//	return View("NewsFeedPartial");
				//}
	}
}