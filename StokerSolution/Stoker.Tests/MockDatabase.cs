using FakeDbSet;
using System.Data.Entity;

using Stoker.Models;
using Stoker.Models.UnionModels;

namespace Stoker.Tests
{
    /// <summary>
    /// This is an example of how we'd create a fake database by implementing the 
    /// same interface that the BookeStoreEntities class implements.
    /// </summary>
    public class MockDatabase : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            comments = new InMemoryDbSet<CommentModel>();
            groups = new InMemoryDbSet<GroupModel>();
            interests = new InMemoryDbSet<InterestModel>();
            threads = new InMemoryDbSet<ThreadModel>();
            userInterestUnion = new InMemoryDbSet<UserInterestUnion>();
            commentLikeUnion = new InMemoryDbSet<CommentLikeUnion>();
            threadLikeUnion = new InMemoryDbSet<ThreadLikeUnion>();
            userCommentUnion = new InMemoryDbSet<UserCommentUnion>();
            userFriendsUnion = new InMemoryDbSet<UserFriendsUnion>();
            userGroupsUnion = new InMemoryDbSet<UserGroupsUnion>();
            threadOriginUnion = new InMemoryDbSet<ThreadOriginUnion>();
        }

        public IDbSet<CommentModel> comments { get; set; }
        public IDbSet<GroupModel> groups { get; set; }
        public IDbSet<InterestModel> interests { get; set; }
        public IDbSet<ThreadModel> threads { get; set; }
        public IDbSet<UserInterestUnion> userInterestUnion { get; set; }
        public IDbSet<CommentLikeUnion> commentLikeUnion { get; set; }
        public IDbSet<ThreadLikeUnion> threadLikeUnion { get; set; }
        public IDbSet<UserCommentUnion> userCommentUnion { get; set; }
        public IDbSet<UserFriendsUnion> userFriendsUnion { get; set; }
        public IDbSet<UserGroupsUnion> userGroupsUnion { get; set; }
        public IDbSet<ThreadOriginUnion> threadOriginUnion { get; set; }

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
           // changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
            //changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}