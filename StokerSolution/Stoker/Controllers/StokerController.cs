using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        //
        // GET: /Stoker/
        public virtual ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderUserImage(string id)
        {
            ApplicationUser user = userService.GetUserByID(id);
            byte[] photoBack = user.image;
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
            string title = Convert.ToString(thread["titleInUserThread"]);
            HttpPostedFileBase file = Request.Files[0];

            model.image = FileToByteArray(file);
            //Image image = thread["imageInUserThread"];
            string content = Convert.ToString(thread["contentInUserThread"]);
            model.title = title;
            var temp = userService.GetUsersByName(User.Identity.Name).First();
            string userID = temp.Id;
            ApplicationUser gettingName = db.Users.FirstOrDefault(x => x.Id == userID);
            model.nameOfPoster = gettingName.firstName + " " + gettingName.lastName;
            model.mainContent = content;
            model.dateCreated = DateTime.Now;

            model.likes = 0;
            model.currentUserLiked = false;

            return model;

        }
	}
}