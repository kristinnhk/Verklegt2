using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;

namespace Stoker.Services
{
    public class ThreadService
    {
         private readonly IAppDataContext db;

        public ThreadService(IAppDataContext context = null)
        {
            db = context ?? new ApplicationDbContext();
        }

        public ICollection<ThreadModel> GetFrontPageThreads(int threadsShown = 0, int orderBy = 0)
        {
            ICollection<ThreadModel> threads = (from t in db.threads
                                                // orderby t.likes descending when like works
                                                //some logic to not show all userprofile threads but only those of friends
                                                //some logic to only show interests followed by user
                                                //some logic to only show group posts user is in
                                                select t).ToList();
            return FilterSkipTake(threads, threadsShown, orderBy);
        }

        public ICollection<ThreadModel> FilterSkipTake(ICollection<ThreadModel> threads, int threadsShown, int orderBy)
        {
            if (orderBy == 0) //show newest first
            {
                return threads.OrderByDescending(ThreadModel => ThreadModel.dateCreated).Skip(threadsShown).Take(5).ToList();
            }
            else if (orderBy == 1) // show oldest first
            {
                return threads.OrderBy(ThreadModel => ThreadModel.dateCreated).Skip(threadsShown).Take(5).ToList();
            }
            else if (orderBy == 2) // show most liked first
            {
                return threads.OrderByDescending(ThreadModel => ThreadModel.likes).Skip(threadsShown).Take(5).ToList();
            }
            else
            {
                return threads;
            }
        }

        /// <summary>
        /// Gets a thread by its ID
        /// </summary>
        /// <param name="threadID">id of the thread</param>
        /// <returns>returns a threadmodel of the thread</returns>
        public ThreadModel GetThreadByID(int threadID)
        {
            return (from t in db.threads
                    where t.threadID == threadID
                    select t).SingleOrDefault();
        }
        
        /// <summary>
        /// Gets all of the threads that have been posted to the group being queried for
        /// </summary>
        /// <param name="groupID">The ID of the group being queried for</param>
        /// <returns>ICollection of threads of a group or null
        ///  if the group does not exist</returns>
        public ICollection<ThreadModel> GetGroupThreads(int groupID, int threadsShown = 0, int orderBy = 0)
        {
            GroupService serviceGroup = new GroupService(db);
            GroupModel group = serviceGroup.GetGroupByID(groupID);
            if (group == null)
            {
                return null;
            }
            if (group.threads == null)
            {
                group.threads = new List<ThreadModel>();
            }
            return FilterSkipTake(group.threads, threadsShown, orderBy);
        }

        /// <summary>
        /// Gets all of the threads that have been posted to the interest being queried for
        /// </summary>
        /// <param name="interestID">The ID of the interest being queried for</param>
        /// <returns></returns>
        public ICollection<ThreadModel> GetInterestThreads(int interestID, int threadsShown = 0, int orderBy = 0)
        {
            InterestService serviceInterest = new InterestService(db);
            InterestModel interest = serviceInterest.GetInterestByID(interestID);
            if (interest == null)
            {
                return null;
            }
            if (interest.threads == null)
            {
                interest.threads = new List<ThreadModel>();
            }
            return FilterSkipTake(interest.threads, threadsShown, orderBy);
        }

        /// <summary>
        /// Gets all of the threads that have been posted by the user to his wall but not to a group or interest
        /// </summary>
        /// <param name="userID">The ID of the user being queried for</param>
        /// <returns>null if user does not exist list of users threads if he does</returns>
        public ICollection<ThreadModel> GetUserThreads(string userID, int threadsShown = 0, int orderBy = 0)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            if (user == null)
            {
                return null;
            }
            if (user.threads == null)
            {
                user.threads = new List<ThreadModel>();
            }
            return FilterSkipTake(user.threads, threadsShown, orderBy);
        }

        /// <summary>
        /// Add the thread to the database and also connect the thread to the group it was posted in
        /// if both exist, else it does nothing.
        /// </summary>
        /// <param name="groupID">The ID of the group that the thread is posted in</param>
        /// <param name="userID">The ID of the user that posted the thread</param>
        /// <param name="model">The thread itself</param>
        public void SetGroupThread(int groupID, string userID, ThreadModel model)
        {
            UserService serviceUser = new UserService(db);
            GroupService serviceGroup = new GroupService(db);

            ApplicationUser threadUser = serviceUser.GetUserByID(userID);
            GroupModel threadGroup = serviceGroup.GetGroupByID(groupID);
            if (threadGroup == null || threadUser == null)
            {
                return;
            }
            if (threadUser.threads == null)
            {
                threadUser.threads = new List<ThreadModel>();
            }
            if (threadGroup.threads == null)
            {
                threadGroup.threads = new List<ThreadModel>();
            }
            model.originalPoster = threadUser;
            model.groupPost = threadGroup;
            threadGroup.threads.Add(model);
            threadUser.threads.Add(model);
            db.threads.Add(model);
            db.SaveChanges();
            
        }

        /// <summary>
        /// Add the thread to the database and also connect the thread to the interest it was posted in
        /// </summary>
        /// <param name="interestID">The ID of the interest that the thread is posted in</param>
        /// <param name="userID">The ID of the user that posted the thread</param>
        /// <param name="model">The thread itself</param>
        public void SetInterestThread(int interestID, string userID, ThreadModel model)
        {
            UserService serviceUser = new UserService(db);
            InterestService serviceInterest = new InterestService(db);

            ApplicationUser threadUser = serviceUser.GetUserByID(userID);
            InterestModel threadInterest = serviceInterest.GetInterestByID(interestID);
            if (threadInterest == null || threadUser == null)
            {
                return;
            }
            if (threadUser.threads == null)
            {
                threadUser.threads = new List<ThreadModel>();
            }
            if (threadInterest.threads == null)
            {
                threadInterest.threads = new List<ThreadModel>();
            }
            model.originalPoster = threadUser;
            model.interestPost = threadInterest;
            threadInterest.threads.Add(model);
            threadUser.threads.Add(model);
            db.threads.Add(model);
            db.SaveChanges();
        }
        
        /// <summary>
        /// Add the thread to the database and also connect the thread to the user who'se wall it was posted on
        /// </summary>
        /// <param name="userID">The ID of the user that posted the thread</param>
        /// <param name="model">The thread itself</param>
        public void SetUserThread(string userID, ThreadModel model)
        {
            UserService serviceUser = new UserService(db);

            ApplicationUser threadUser = serviceUser.GetUserByID(userID);
            if (threadUser == null)
            {
                return;
            }
            if (threadUser.threads == null)
            {
                threadUser.threads = new List<ThreadModel>();
            }
            model.originalPoster = threadUser;
            model.profilePost = threadUser;
            threadUser.threads.Add(model);
            db.threads.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// Get the comments of a thread by ID
        /// </summary>
        /// <param name="threadID">ID of the thread</param>
        /// <returns>returns a collection of comments
        /// of the thread if the thread exists and null otherwise</returns>
        public ICollection<CommentModel> GetThreadComments(int threadID)
        {
            ThreadModel thread = GetThreadByID(threadID);
            if (thread == null)
            {
                return null;
            }
            if (thread.comments == null)
            {
                thread.comments = new List<CommentModel>();
            }
            return thread.comments;
        }

        /// <summary>
        /// Adds a new comment to a thread
        /// </summary>
        /// <param name="threadID">id of the thread</param>
        /// <param name="userID">id of the poster</param>
        /// <param name="model">the comment model</param>
        public void SetThreadComment(int threadID, string userID, CommentModel model)
        {
            UserService serviceUser = new UserService(db);

            ApplicationUser threadUser = serviceUser.GetUserByID(userID);
            if (threadUser == null)
            {
                return;
            }
            ThreadModel thread = GetThreadByID(threadID);
            if(thread == null){
                return;
            }
            if (thread.comments == null)
            {
                thread.comments = new List<CommentModel>();
            }
            thread.comments.Add(model);
            model.commentAuthor = threadUser;
            if (model.likes != 0)
            {
                model.likes = 0;
            }
            model.thread = thread;
            db.comments.Add(model);
            db.SaveChanges();
        }




    }
}