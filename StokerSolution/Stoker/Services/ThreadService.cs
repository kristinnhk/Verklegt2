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

        public ICollection<ThreadModel> GetFilteredThreads(string userID, int threadsShown = 0, int orderBy = 0, int filterBy = 0)
        {
            if(filterBy == 1){
                ICollection<ThreadModel> threads = GetUserGroupsThreads(userID);
                return FilterSkipTake(threads, threadsShown, orderBy);
            }
            else if(filterBy == 2){
                ICollection<ThreadModel> threads = GetUserInterestsThreads(userID);
                return FilterSkipTake(threads, threadsShown, orderBy);
            }
            else if (filterBy == 3)
            {
                ICollection<ThreadModel> threads = GetUserFriendsThreads(userID);
                return FilterSkipTake(threads, threadsShown, orderBy);
            }
            else if(filterBy == 0)
            {
                ICollection<ThreadModel> threads = (from t in db.threads
                                                    // orderby t.likes descending when like works
                                                    //some logic to NOT show all userprofile threads that ARENT freinds
                                                    //some logic to NOT show group posts user ISNT in
                                                    select t).ToList();
                return FilterSkipTake(threads, threadsShown, orderBy);
            }
            else
            {
                ICollection<ThreadModel> threads = GetUserThreads(userID);
                return FilterSkipTake(threads, threadsShown, orderBy);
            }
            
        }

        /// <summary>
        /// Gets the threads of all groups a user is a member of
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <returns>ICollection of threads posted in groups the user is a member of</returns>
        public ICollection<ThreadModel> GetUserGroupsThreads(string userID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            if (user.groups == null)
            {
                return new List<ThreadModel>();
            }
            List<ThreadModel> threads = new List<ThreadModel>();
            foreach (GroupModel groups in user.groups)
            {
                List<ThreadModel> temp = (from thread in db.threads
                                         where thread.groupPost.groupID == groups.groupID
                                         select thread).ToList();
                threads.AddRange(temp);
            } 
            return threads;
        }

        /// <summary>
        /// Gets the threads of all interests a user is a member of
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <returns>ICollection of threads posted in interests the user is a member of</returns>
        public ICollection<ThreadModel> GetUserInterestsThreads(string userID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            if (user.interests == null)
            {
                return new List<ThreadModel>();
            }
            List<ThreadModel> threads = new List<ThreadModel>();
            foreach (InterestModel interests in user.interests)
            {
                List<ThreadModel> temp = (from thread in db.threads
                                          where thread.interestPost.interestID == interests.interestID
                                          select thread).ToList();
                threads.AddRange(temp);
            }
            return threads;
        }

        /// <summary>
        /// Gets the threads posted by the users friends.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <returns>ICollection of threads posted by the users friends or an empty collection 
        /// if no threads exist</returns>
        public ICollection<ThreadModel> GetUserFriendsThreads(string userID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            if (user == null)
            {
                return new List<ThreadModel>();
            }
            if (user.friendRequestSent == null || user.friendRequestReceived == null)
            {
                return new List<ThreadModel>();
            }
            List<ThreadModel> threads = new List<ThreadModel>();
            // Collect a list of user friends

            List<ApplicationUser> userFriends = (from rUser in user.friendRequestReceived
                                                join sUser in user.friendRequestSent on rUser.Id equals sUser.Id
                                                select rUser).ToList();
            // 
            foreach (ApplicationUser users in userFriends)
            {
                List<ThreadModel> temp = (from thread in db.threads
                                          where thread.originalPoster.Id == users.Id
                                          select thread).ToList();
                threads.AddRange(temp);
            }
            return threads;
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
            ThreadModel thread = (from t in db.threads
                                  where t.threadID == threadID
                                 select t).SingleOrDefault();
            return thread;
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
            return user.threads;
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

            if (model.title == "" || model.mainContent == "")
            {
                return;
            }

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

            if (model.title == "" || model.mainContent == "")
            {
                return;
            }

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
            if (model.title == "" || model.mainContent == "")
            {
                return;
            }

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
        /// gets a comment from the database based on its ID
        /// </summary>
        /// <param name="commentID">id of the comment</param>
        /// <returns>commentmodel of the comment</returns>
        public CommentModel GetCommentByID(int commentID)
        {
            return (from comment in db.comments
                    where comment.commentID == commentID
                    select comment).SingleOrDefault();
        }

        /// <summary>
        /// Adds a new comment to a thread
        /// </summary>
        /// <param name="threadID">id of the thread</param>
        /// <param name="userID">id of the poster</param>
        /// <param name="model">the comment model</param>
        public void SetThreadComment(int threadID, string userID, CommentModel model)
        {
            if (model.content == "")
            {
                return;
            }
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

        /// <summary>
        /// Adds a user to a list of users that have liked the thread.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="threadID">id of the thread</param>
        public void LikeThread(string userID, int threadID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            ThreadModel thread = GetThreadByID(threadID);
            if (user == null)
            {
                return;
            }
            if (thread.usersLiked == null)
            {
                thread.usersLiked = new List<ApplicationUser>();
            }
            string hasLiked = (from t in thread.usersLiked
                               where t.Id == user.Id
                               select t.Id).SingleOrDefault();
            if (hasLiked == null)
            {
                thread.likes += 1;
                thread.usersLiked.Add(user);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// deletes a user from a list of users that have liked a thread.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="threadID">id of the thread</param>
        public void UnLikeThread(string userID, int threadID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            ThreadModel thread = GetThreadByID(threadID);
            if (user == null)
            {
                return;
            }
            if (thread.usersLiked == null)
            {
                thread.usersLiked = new List<ApplicationUser>();
                return;
            }
            if (thread.usersLiked.Remove(user) == true)
            {
                thread.likes -= 1;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// checks if a user has liked a thread
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="threadID">id of the thread</param>
        /// <returns>true if user has liked a thread false otherwise.</returns>
        public bool UserHasLikedThread(string userID, int threadID)
        {
            ThreadModel thread = GetThreadByID(threadID);
            ApplicationUser liked = (from u in thread.usersLiked
                                         where u.Id == userID
                                         select u).SingleOrDefault();
            if (liked == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Adds a user to a list of users that have liked the comment.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="commentID">id of the thread</param>
        public void LikeComment(string userID, int commentID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            CommentModel comment = GetCommentByID(commentID);
            if (user == null)
            {
                return;
            }
            if (comment.usersLiked == null)
            {
                comment.usersLiked = new List<ApplicationUser>();
            }
            comment.usersLiked.Add(user);
            comment.likes += 1;
            db.SaveChanges();
        }

        /// <summary>
        /// deletes a user from a list of users that have liked a comment.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="commentID">id of the comment</param>
        public void UnLikeComment(string userID, int commentID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            CommentModel comment = GetCommentByID(commentID);
            if (user == null)
            {
                return;
            }
            if (comment.usersLiked == null)
            {
                comment.usersLiked = new List<ApplicationUser>();
                return;
            }
            if (comment.usersLiked.Remove(user) == true)
            {
                comment.likes -= 1;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// checks if a user has liked a comment
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="commentID">id of the comment</param>
        /// <returns>true if user has liked a comment false otherwise.</returns>
        public bool UserHasLikedComment(string userID, int commentID)
        {
            CommentModel comment = GetCommentByID(commentID);
            ApplicationUser liked = (from u in comment.usersLiked
                                     where u.Id == userID
                                     select u).SingleOrDefault();
            if (liked == null)
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