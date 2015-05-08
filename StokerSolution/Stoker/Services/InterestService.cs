using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;
using Stoker.Models.UnionModels;

namespace Stoker.Services
{
    public class InterestService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// returns an interestmodel with the given id from the database using LINQ
        /// </summary>
        /// <param name="interestID">The id used to look for an interest in the db</param>
        /// <returns></returns>
        public InterestModel GetInterestByID(int interestID)
        {
            InterestModel Interest = (from i in db.interests
                         where i.interestID == interestID
                         select i).FirstOrDefault();

            return Interest;
        }

        /// <summary>
        /// Uses GetInterestByID method to get the interest with the given id and then replace it's
        /// title with the one given in the parameters. if no interest is found we do nothing.
        /// </summary>
        /// <param name="interestID">The id of the interest we want to change</param>
        /// <param name="title">The new title</param>
        public void SetInterestTitle(int interestID, string title)
        {
            try { 
                GetInterestByID(interestID).name = title;
            }catch(Exception){
                return;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// This function returns a list of users who are interested in the interest of the given id
        /// Dude to how the union tables work, the FK's are directly pointing to the model itself
        /// The FK names are a bit off though, since the entity framework made them for us using code first
        /// </summary>
        /// <param name="interestID">the id of the interest we are looking to find the users for</param>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> GetInterestUsers(int interestID)
        {
            IEnumerable<ApplicationUser> users = (from u in db.userInterestUnion
                                        where u.interestID.interestID == interestID
                                        select u.User);

            return users;
        }

    }
}