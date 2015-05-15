﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;
using Stoker.Services;

namespace Stoker.Services
{
    public class InterestService
    {

         private readonly IAppDataContext db;

        public InterestService(IAppDataContext context = null)
        {
            db = context ?? new ApplicationDbContext();
        }

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
        /// This function returns a list of users who are interested in the interest of the given id
        /// </summary>
        /// <param name="interestID">the id of the interest we are looking to find the users for</param>
        /// <returns>Returns a collection of Application users,
        ///  if the interest with that id does not exist this function returns null</returns>
        public ICollection<ApplicationUser> GetInterestUsers(int interestID)
        {
            InterestModel interest = GetInterestByID(interestID);
            if (interest == null)
            {
                return null;
            }
            if (interest.users == null)
            {
                interest.users = new List<ApplicationUser>();
            }
            return interest.users;
        }

        /// <summary>
        /// Returns the interests a user is a member of
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <returns>Icollection of interests the user is a member of
        ///  if this user does not exist returns null</returns>
        public ICollection<InterestModel> GetUserInterests(string userID)
        {
            UserService service = new UserService(db);

            ApplicationUser user = service.GetUserByID(userID);
            if (user != null)
            {
                return user.interests;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new interest to the database. Be advised that when you call this
        /// function it should already have at least one member. That member should be the interest
        /// creator.
        /// </summary>
        /// <param name="interest"></param>
        public void SetNewInterest(InterestModel interest)
        {
            if (interest.users == null)
            {
                interest.users = new List<ApplicationUser>();
            }
            db.interests.Add(interest);
            db.SaveChanges();
        }

        /// <summary>
        /// connect a user to an interest he is interested in
        /// </summary>
        /// <param name="interestID">The ID of the interest the user is interesed in</param>
        /// <param name="userID">The ID of the user who is interest in something</param>
        public void SetUserInterest(int interestID, string userID)
        {
            UserService service = new UserService(db);

            ApplicationUser user = service.GetUserByID(userID);
            InterestModel interest = GetInterestByID(interestID);
            //if the user or interest does not exist we do not add to the database.
            if (user == null || interest == null)
            {
                return;
            }
            if (user.interests == null)
            {
                user.interests = new List<InterestModel>();
            }
            if (interest.users == null)
            {
                interest.users = new List<ApplicationUser>();
            }
            user.interests.Add(interest);
            interest.users.Add(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Gets all of the interests that include the search string
        /// it matches partially and selects all that match
        /// </summary>
        /// <param name="title">The search string</param>
        /// <returns></returns>
        public IEnumerable<InterestModel> GetInterestsByTitle(string title)
        {
            IEnumerable<InterestModel> interests = from i in db.interests
                                                   where i.name.Contains(title)
                                                   select i;
            return interests;
        }

        /// <summary>
        /// Deletes a user from an interest
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="interestID">id of the interest</param>
        public void DeleteUserInterest(string userID, int interestID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            InterestModel interest = GetInterestByID(interestID);
            user.interests.Remove(interest);
            interest.users.Remove(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Returns true if user is following an interest
        /// but false otherwise
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="interestID">id of the interest</param>
        /// <returns>Returns true if user is following an interest
        /// but false otherwise</returns>
        public bool IsFollowingInterest(string userID, int interestID)
        {
            InterestModel interest = GetInterestByID(interestID);
            ApplicationUser following = (from u in interest.users
                             where u.Id == userID
                             select u).SingleOrDefault();
            if (following == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}