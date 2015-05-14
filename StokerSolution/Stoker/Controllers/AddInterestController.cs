﻿using Stoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using Stoker.Services;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Stoker.Controllers
{
    public class AddInterestController : StokerController
    {
        // GET: AddInterest
        public ActionResult AddInterest()
        {
            return View();
        }
         public ActionResult Add(FormCollection collection)
        {
            InterestModel model = new InterestModel();

            model.name = collection["nameInInterest"];

            string userID = User.Identity.GetUserId();
            ApplicationUser user = userService.GetUserByID(userID);
            model.users = new List<ApplicationUser>();
            model.users.Add(user);
            
            model.numberOfUsersInterested = 1;
            interestService.SetNewInterest(model);
            return RedirectToAction("Interest", "Interest", new { interestId =  model.interestID});
        }
	
    }
}