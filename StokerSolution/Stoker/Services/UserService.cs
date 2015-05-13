using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Linq.Expressions;

using Microsoft.SqlServer.Server;

using Stoker.Models;



namespace Stoker.Services
{
    public class UserService
    {
         private readonly IAppDataContext db;

        public UserService(IAppDataContext context = null)
        {
            db = context ?? new ApplicationDbContext();
        }

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
            string pathPrefix = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images"), "default-photo.png");
            System.Drawing.Image imageIn = System.Drawing.Image.FromFile(pathPrefix);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            GetUserByID(userID).image = ms.ToArray();
            db.SaveChanges();
        }

        /// <summary>
        /// Sends a friend request from one user to another
        /// </summary>
        /// <param name="userSendingID">user sending request</param>
        /// <param name="userReceivingID">user receiving request</param>
        public void SendFriendRequest(string userSendingID, string userReceivingID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser sender = serviceUser.GetUserByID(userSendingID);
            ApplicationUser receiver = serviceUser.GetUserByID(userReceivingID);
            sender.friendRequestSent.Add(receiver);
            receiver.friendRequestReceived.Add(sender);
            db.SaveChanges();
        }

        /// <summary>
        /// Accepts a friend request from another user.
        /// </summary>
        /// <param name="userAcceptingID">User acting and accepting the request</param>
        /// <param name="friendID">user that previously sent the request.</param>
        public void AcceptFriendRequest(string userAcceptingID, string friendID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser accepter = serviceUser.GetUserByID(userAcceptingID);
            ApplicationUser friend = serviceUser.GetUserByID(friendID);
            accepter.friendRequestSent.Add(friend);
            friend.friendRequestReceived.Add(accepter);
            db.SaveChanges();
        }

        /// <summary>
        /// Returns the friend requests this user has received
        /// </summary>
        /// <param name="userID">id of said user</param>
        /// <returns>IEnumerable of application users that have sent him requests</returns>
        public IEnumerable<ApplicationUser> GetFriendRequests(string userID)
        {
            /*Below is code getting only friend requests gotten that this user has not answered,
             * this corresponds to these calculations where A is sentRequests, B is gottenRequests
             * C is xORSentGotten, D is unionXorSent and E is returnResult.
             * A B 
             * C = A XOR B
             * D = C U A
             * E = D XOR A
             * E = B but not and not both A and B
            */ 
            UserService serviceUser = new UserService(db);
            ApplicationUser user = GetUserByID(userID);
            IEnumerable<ApplicationUser> sentRequests = user.friendRequestSent;
            IEnumerable<ApplicationUser> gottenRequests = user.friendRequestReceived.ToList();
            //filtering users only in one list.
            IEnumerable<ApplicationUser> xORSentGotten = gottenRequests.Where(p => !sentRequests.Any(p2 => p2.Id == p.Id));
            List<ApplicationUser> bla = xORSentGotten.ToList();
            IEnumerable<ApplicationUser> unionXorSent = xORSentGotten.Union(sentRequests);
            //filtering users only in the gottenRequests list but not both.
            IEnumerable<ApplicationUser> returnResult = unionXorSent.Where(p => !sentRequests.Any(p2 => p2.Id == p.Id));
            return returnResult;
        }

        /// <summary>
        /// Checks if a current user has sent another user a friend request
        /// </summary>
        /// <param name="currentUserID">user that sends requests</param>
        /// <param name="otherUserID">user receiving requests</param>
        /// <returns>true if current user has sent other user a friend request
        /// false otherwise</returns>
        public bool FriendRequestSent(string currentUserID, string otherUserID)
        {
            ApplicationUser currentUser = GetUserByID(currentUserID);
            ApplicationUser otherUser = GetUserByID(otherUserID);
            var friendSent = (from ApplicationUser user in currentUser.friendRequestSent 
                              where user.Id == otherUserID
                              select user).SingleOrDefault();
            if (friendSent == otherUser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}