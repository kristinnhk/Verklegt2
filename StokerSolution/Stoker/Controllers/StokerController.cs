using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Stoker.Models;
using Stoker.Services;
using System.Drawing;
using System.IO;


namespace Stoker.Controllers
{
    public class StokerController : Controller
    {

        protected ApplicationDbContext db = new ApplicationDbContext();
        protected UserService userService;
        protected GroupService groupService;
        protected InterestService interestService;
        protected ThreadService threadService;
        //
        // GET: /Stoker/

        public StokerController()
        {
            userService = new UserService(db);
            groupService = new GroupService(db);
            interestService = new InterestService(db);
            threadService = new ThreadService(db);
        }
        public virtual ActionResult StopAmbigiousNameError()
        {
            return View();
        }

        public ActionResult RenderThreadImage(int id)
        {

            ThreadModel thread = threadService.GetThreadByID(id);

            byte[] photoBack = thread.image;
            return File(photoBack, "image/png");
        }

        public ActionResult RenderUserImage(string id)
        {
            ApplicationUser user = userService.GetUserByID(id);
            byte[] photoBack = user.image;
            return File(photoBack, "image/png");
        }
        public ActionResult RenderGroupImage(int id)
        {
            GroupModel group = groupService.GetGroupByID(id);
            byte[] photoBack = group.image;
            return File(photoBack, "image/png");
        }

        public byte[] FileToByteArray(HttpPostedFileBase file)
        {
            Image imageIn = Image.FromStream(file.InputStream, true, true);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public ThreadModel FillThreadModel(FormCollection thread)
        {
            ThreadModel model = new ThreadModel();
 
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength != 0)
            {
                model.image = FileToByteArray(file);
            }
  
            model.title = Convert.ToString(thread["titleInThread"]);
            string userID = User.Identity.GetUserId();
            ApplicationUser gettingName = db.Users.FirstOrDefault(x => x.Id == userID);
            model.originalPoster = gettingName;
            model.mainContent = Convert.ToString(thread["contentInThread"]);
            model.dateCreated = DateTime.Now;
            model.likes = 0;

            return model;
        }


        public ActionResult GetMoreNews()
        {
            int filterBy = Convert.ToInt32(Request["filterBy"]);
            int orderBy = Convert.ToInt32(Request["orderBy"]);
            int threadsShown = Convert.ToInt32(Request["threadsShown"]);



            ViewModel model = new ViewModel();
            model.groups = new List<GroupModel>();
            model.interests = new List<InterestModel>();
            model.Users = new List<ApplicationUser>();
            model.threads = new List<ThreadModel>();
            byte[] bit = new byte[] { 0x1 }; // Used so Json request doesnt get too large
            List<ThreadModel> templist = new List<ThreadModel>();
            if (filterBy == -1)
            {
                string profileID = Request["profileID"];
                ApplicationUser temp = userService.GetUserByID(profileID);
                templist = threadService.GetFrontPageThreads(temp.Id, threadsShown, orderBy, filterBy).ToList();
            }
            else
            {
                templist = threadService.GetFrontPageThreads(User.Identity.GetUserId(), threadsShown, orderBy, filterBy).ToList();
            }

            foreach (var item in templist)
            {
                ThreadModel newModel = new ThreadModel();
                newModel.title = item.title;
                newModel.likes = item.likes;
                newModel.dateCreated = item.dateCreated;
                newModel.mainContent = item.mainContent;
                if (item.image != null)
                {
                    newModel.image = bit;
                }
                newModel.threadID = item.threadID;
                newModel.originalPoster = new ApplicationUser();
                newModel.originalPoster.Id = item.originalPoster.Id;
                newModel.originalPoster.firstName = item.originalPoster.firstName;
                newModel.originalPoster.lastName = item.originalPoster.lastName;
                newModel.usersLiked = item.usersLiked;
                //   newModel.profilePost = item.profilePost;
                //   newModel.interestPost = item.interestPost;
                //  newModel.groupPost = item.groupPost;
                //  newModel.comments = item.comments;
                model.threads.Add(newModel);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Checks if current user has liked a thread
        /// </summary>
        /// <returns>true if user has liked</returns>
        public int IsLikedThread()
        {
            string userID = User.Identity.GetUserId();
             int threadID = Convert.ToInt32(Request["threadID"]);
            bool isLiked = threadService.UserHasLikedThread(userID, threadID);
            if (isLiked == true)
            {
                return threadID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// lets user like a thread
        /// </summary>
        [HttpPost]
        public void LikeThread()
        {
            string userID = User.Identity.GetUserId();
            int threadID = Convert.ToInt32(Request["threadID"]);
            threadService.LikeThread(userID, threadID);
        }

        /// <summary>
        /// lets user unlike a thread
        /// </summary>
        [HttpPost]
        public void UnLikeThread()
        {
            string userID = User.Identity.GetUserId();
            int threadID = Convert.ToInt32(Request["threadID"]);
            threadService.UnLikeThread(userID, threadID);
        }

        /// <summary>
        /// checks if user has liked a comment
        /// </summary>
        /// <returns>true if user has liked the comment</returns>
        [HttpPost]
        public bool IsLikedComment()
        {
            string userID = User.Identity.GetUserId();
            int threadID = Convert.ToInt32(Request["threadID"]);
            return threadService.UserHasLikedComment(userID, threadID);
        }


        /// <summary>
        /// lets user like a comment
        /// </summary>
        public void LikeComment()
        {
            string userID = User.Identity.GetUserId();
            int threadID = Convert.ToInt32(Request["threadID"]);
            threadService.LikeComment(userID, threadID);
        }

        /// <summary>
        /// lets a user unlike a comment
        /// </summary>
        public void UnLikeComment()
        {
            string userID = User.Identity.GetUserId();
            int threadID = Convert.ToInt32(Request["threadID"]);
            threadService.UnLikeComment(userID, threadID);
        }

        [HttpPost]
        public int NumberOfLikes()
        {
            int threadID = Convert.ToInt32(Request["threadID"]);
            ThreadModel thread = threadService.GetThreadByID(threadID);
            return thread.likes;
        }
	}
}