using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;

namespace Stoker.Services
{
    public class ThreadService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

      /*  public IEnumerable<ThreadModel> GetGroupThreads(int groupID)
        {
            IEnumerable<ApplicationUser> users = (from u in db.userInterestUnion
                                        where u.interestID.interestID == interestID
                                        select u.User);

          //  return users;

            IEnumerable<ThreadModel> threads = from t in db.threadOriginUnion
                                                where t.Group.groupID == groupID 
                                                select t.


            return threads;
        }*/
    }
}