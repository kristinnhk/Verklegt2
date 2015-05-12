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
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserService userService = new UserService();
        private GroupService groupService = new GroupService();
        private InterestService interestService = new InterestService();
        private ThreadService threadservice = new ThreadService();
        //
        // GET: /Stoker/
        public virtual ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderThreadImage(int id)
        {

            ThreadModel thread = threadservice.GetThreadByID(id);

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
            model.nameOfPoster = gettingName.firstName + " " + gettingName.lastName;
            model.mainContent = Convert.ToString(thread["contentInThread"]);
            model.dateCreated = DateTime.Now;
            model.likes = 0;
            model.currentUserLiked = false;

            return model;

        }
	}
}