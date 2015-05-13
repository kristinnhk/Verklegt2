using FakeDbSet;
using System.Data.Entity;

using Stoker.Models;


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
            Users = new InMemoryDbSet<ApplicationUser>();
            comments = new InMemoryDbSet<CommentModel>();
            groups = new InMemoryDbSet<GroupModel>();
            interests = new InMemoryDbSet<InterestModel>();
            threads = new InMemoryDbSet<ThreadModel>();

        }

        public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<CommentModel> comments { get; set; }
        public IDbSet<GroupModel> groups { get; set; }
        public IDbSet<InterestModel> interests { get; set; }
        public IDbSet<ThreadModel> threads { get; set; }

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