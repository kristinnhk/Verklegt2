using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;
using Stoker.Models.UnionModels;

namespace Stoker.Services
{
    public class ThreadService
    {
         private readonly IAppDataContext db;

        public ThreadService(IAppDataContext context = null)
        {
            db = context ?? new ApplicationDbContext();
        }

        public IEnumerable<ThreadModel> GetLatestThreadsAll()
        {
            IEnumerable<ThreadModel> threads = (from t in db.threads
                                                orderby t.dateCreated descending
                                                select t).Take(10);
            return threads;
        }

        /// <summary>
        /// Gets all of the threads that have been posted to the group being queried for
        /// </summary>
        /// <param name="groupID">The ID of the group being queried for</param>
        /// <returns></returns>
        public IEnumerable<ThreadModel> GetGroupThreads(int groupID)
        {
           
            IEnumerable<ThreadModel> threads = from t in db.threadOriginUnion
                                               where t.Group.groupID == groupID
                                               select t.Thread;
            return threads;
        }

        public ThreadModel GetThreadByID(int threadID)
        {
            ThreadModel thread = (from t in db.threads
                                 where t.threadID == threadID
                                 select t).SingleOrDefault();
            return thread;
        }

        /// <summary>
        /// Gets all of the threads that have been posted to the interest being queried for
        /// </summary>
        /// <param name="interestID">The ID of the interest being queried for</param>
        /// <returns></returns>
        public IEnumerable<ThreadModel> GetInterestThreads(int interestID)
        {

            IEnumerable<ThreadModel> threads = from t in db.threadOriginUnion
                                               where t.interest.interestID == interestID
                                               select t.Thread;
            return threads;
        }

        /// <summary>
        /// Gets all of the threads that have been posted by the user to his wall but not to a group or interest
        /// </summary>
        /// <param name="userID">The ID of the user being queried for</param>
        /// <returns></returns>
        public IEnumerable<ThreadModel> GetUserThreads(string userID)
        {

            IEnumerable<ThreadModel> threads = from t in db.threadOriginUnion
                                               where t.User.Id == userID
                                               && t.interest == null
                                               && t.Group == null
                                               select t.Thread;
            return threads;
        }

     /*   public IEnumerable<ThreadModel> GetUserFriendsThreads(string userID)
        {
        has not been programmed..................................
            IEnumerable<ThreadModel> threads = from t in db.threadOriginUnion
                                               where t.User.Id == userID
                                               && t.interest == null
                                               && t.Group == null
                                               select t.Thread;
            return threads;
        }*/




        /// <summary>
        /// Add the thread to the database and also connect the thread to the group it was posted in
        /// </summary>
        /// <param name="groupID">The ID of the group that the thread is posted in</param>
        /// <param name="userID">The ID of the user that posted the thread</param>
        /// <param name="model">The thread itself</param>
        public void SetGroupThread(int groupID, string userID, ThreadModel model)
        {
            db.threads.Add(model);
            db.SaveChanges();
            ThreadOriginUnion union = new ThreadOriginUnion();
           
            union.Group = db.groups.FirstOrDefault(x => x.groupID == groupID);
            union.Thread = model;
            union.User = db.Users.FirstOrDefault(x => x.Id == userID);
            db.threadOriginUnion.Add(union);
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
            db.threads.Add(model);
            db.SaveChanges();
            ThreadOriginUnion union = new ThreadOriginUnion();

            union.interest = db.interests.FirstOrDefault(x => x.interestID == interestID);
            union.Thread = model;
            union.User = db.Users.FirstOrDefault(x => x.Id == userID);
            db.threadOriginUnion.Add(union);
            db.SaveChanges();

        }
        /// <summary>
        /// Add the thread to the database and also connect the thread to the user who'se wall it was posted on
        /// </summary>
        /// <param name="userID">The ID of the user that posted the thread</param>
        /// <param name="model">The thread itself</param>
        public void SetUserThread(string userID, ThreadModel model)
        {
            db.threads.Add(model);
            db.SaveChanges();
            ThreadOriginUnion union = new ThreadOriginUnion();
            union.Thread = model;
            union.User = db.Users.FirstOrDefault(x => x.Id == userID);
            db.threadOriginUnion.Add(union);
            db.SaveChanges();
        }

        public IEnumerable<CommentModel> GetThreadComments(int threadID)
        {
            IEnumerable<CommentModel> comments = from c in db.userCommentUnion
                                                 where c.Thread.threadID == threadID
                                                 orderby c.comment.dateCreated ascending
                                                 select c.comment;
            return comments;
        }

        public void SetThreadComment(int threadID, string userID, CommentModel model)
        {
            db.comments.Add(model);
            db.SaveChanges();
            UserCommentUnion union = new UserCommentUnion();
            union.User = db.Users.FirstOrDefault(x => x.Id == userID);
            union.comment = model;
            union.Thread = db.threads.FirstOrDefault(x => x.threadID == threadID);
            db.userCommentUnion.Add(union);
            db.SaveChanges();
        }




    }
}