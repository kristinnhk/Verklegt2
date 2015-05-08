using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;

namespace Stoker.Services
{
    public class UserService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This function Gets users from the database by searching the firstName and lastName
        /// fields sorted alphabetically by last name.
        /// </summary>
        /// <param name="name">Name is the string searched for</param>
        /// <returns>this function returns a list of application users
        /// sorted in alphabetical order by last name.</returns>
        public IEnumerable<ApplicationUser> GetUsersByName(string name)
        {
            IEnumerable<ApplicationUser> userList = (from user in db.Users
                                               where user.firstName.Contains(name)
                                               select user).Union
                                              (from userLast in db.Users
                                               where userLast.lastName.Contains(name)
                                               select userLast)
                                              ;
            IEnumerable<ApplicationUser> userReturnList = userList.OrderBy(user => user.lastName);
            return userReturnList;
        }

        /// <summary>
        /// Searches the database users by their ID
        /// </summary>
        /// <param name="ID"> matches the Users.Id row in the Users table in the database</param>
        /// <returns>Returns an ApplicationUser or null if no user exists with that particular ID</returns>
        public ApplicationUser GetUserByID(string ID)
        {
            return (from user in db.Users
                    where user.Id == ID
                    select user).SingleOrDefault();
        }

        public void ChangeAboutMe(string userID, string aboutMe)
        {
            try
            {
                GetUserByID(userID).about = aboutMe;
                db.SaveChanges();
                return;
            }
            catch
            {
                return;
            }
        }
    }
}