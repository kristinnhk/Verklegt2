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
    [HandleError]
    [Authorize]
    public class StokerController : Controller
    {

        protected ApplicationDbContext db = new ApplicationDbContext();
        protected UserService userService;
        protected GroupService groupService;
        protected InterestService interestService;
        protected ThreadService threadService;

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

        /// <summary>
        /// These 3 similar functions render pictures to the view
        /// by getting the image seperately 
        /// this helps alot with ajax calls because you dont have to send the 
        /// entire image files through Json
        /// </summary>
        /// <param name="id">Id of the thread we we are rending image for</param>
        /// <returns></returns>
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

        /// <summary>
        /// we use memorystream to turn the image into a byte[]
        /// </summary>
        /// <param name="file">This is the image we get from the user</param>
        /// <returns></returns>
        public byte[] FileToByteArray(HttpPostedFileBase file)
        {
            if (file.ContentLength == 0)
            {
                string userID = User.Identity.GetUserId();
                ApplicationUser user = userService.GetUserByID(userID);
                return user.image;
            }
            Image imageIn = Image.FromStream(file.InputStream, true, true);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        /// <summary>
        /// This function is simply to reduce refactoring of code
        /// basically it gets all the values from the form to make a thread
        /// </summary>
        /// <param name="thread"> the form we take in</param>
        /// <returns></returns>
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

        /// <summary>
        /// This function gets called by the ajax call when someone wants to load more news.
        /// There are filtering options in the html wich we cherry pick to send to our servicelayer.
        /// ultimately when we get the data we want from the service, we cherry pick only the data we HAVE to ahve
        /// to make it as lightweight as possible and to reduce loading times
        /// images for example are not needed and we render them using aforementioned functions
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMoreNews()
        {
            int filterBy = Convert.ToInt32(Request["filterBy"]);
            int orderBy = Convert.ToInt32(Request["orderBy"]);
            int threadsShown = Convert.ToInt32(Request["threadsShown"]);
            ViewModel model = new ViewModel();
            model.threads = new List<ThreadModel>();
            byte[] bit = new byte[] { 0x1 }; // Used so Json request doesnt get too large
            List<ThreadModel> templist = new List<ThreadModel>();
            if (filterBy == -1)
            {
                string profileID = Request["profileID"];
                ApplicationUser temp = userService.GetUserByID(profileID);
                templist = threadService.GetFilteredThreads(temp.Id, threadsShown, orderBy, filterBy).ToList();
            }
            else
            {
                templist = threadService.GetFilteredThreads(User.Identity.GetUserId(), threadsShown, orderBy, filterBy).ToList();
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
                model.threads.Add(newModel);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Checks if current user has liked a thread
        /// </summary>
        /// <returns>true if user has liked</returns>
        public bool IsLikedThread()
        {
            string userID = User.Identity.GetUserId();
             int threadID = Convert.ToInt32(Request["threadID"]);
            bool isLiked = threadService.UserHasLikedThread(userID, threadID);
            return isLiked;
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