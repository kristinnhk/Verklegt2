using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;
using System.IO;


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

        /// <summary>
        /// Changes the about me section of the current user
        /// </summary>
        /// <param name="tempID">current user ID</param>
        /// <param name="aboutMe">the new about me text you want to set</param>
        public void SetAboutMe(string tempID, string aboutMe)
        {
            try
            {
                GetUserByID(tempID).about = aboutMe;
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes the first name of the current user
        /// </summary>
        /// <param name="tempID">current user ID</param>
        /// <param name="firstName">the new first name you want to set</param>
        public void SetFirstName(string tempID, string firstName)
        {
            try
            {
                GetUserByID(tempID).firstName = firstName;
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes the last name of the current user
        /// </summary>
        /// <param name="tempID">current user ID</param>
        /// <param name="lastName">the new last name you want to set</param>
        public void SetLastName(string tempID, string lastName)
        {
            try
            {
                GetUserByID(tempID).lastName = lastName;
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes the gender of the current user
        /// </summary>
        /// <param name="tempID">current user ID</param>
        /// <param name="gender">the new gender you want to set</param>
        public void SetGender(string tempID, string gender)
        {
            try
            {
                GetUserByID(tempID).gender = gender;
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes image of the current user
        /// </summary>
        /// <param name="tempID">current user ID</param>
        /// <param name="image">the new image you want to set</param>
        public void SetImage(string userID, byte[] image)
        {
            try
            {
                GetUserByID(userID).image = image;
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }

        public void SetImageDefault(string userID)
        {
            System.Drawing.Image imageIn = System.Drawing.Image.FromFile(@"C:\Users\Kristinn\Dropbox\HR\HR.2.onn\Verklegt_namskeid_2\ProjectX\Verklegt2\StokerSolution\Stoker\Content\Images\default-Photo.png");
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            GetUserByID(userID).image = ms.ToArray();
            db.SaveChanges();
        }
    }
}