﻿using System;
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
	}
}