using Stoker.Models;
using Stoker.Models.UnionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Stoker.Services
{
    public class InterestService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public string GetInterestTitle(int interestID)
        {
            var title = (from i in db.interests
                         where i.interestID == interestID
                         select i.name).FirstOrDefault();

            return title;
        }


    }
}