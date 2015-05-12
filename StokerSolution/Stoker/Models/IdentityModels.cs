using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Stoker.Models.UnionModels;
using System.IO;
using System.Collections.Generic;

namespace Stoker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
  //      [Required]
        public string firstName { get; set; }
 //       [Required]
        public string lastName { get; set; }
        public string gender { get; set; }
        public string about { get; set; }
        public byte[] image { get; set; }
        public virtual ICollection<GroupModel> groups { get; set; }
        public virtual ICollection<InterestModel> interests { get; set; }
        public virtual ICollection<ThreadModel> threads { get; set; }
        public virtual ICollection<ApplicationUser> friendRequestSent { get; set; }
        public virtual ICollection<ApplicationUser> friendRequestReceived { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public interface IAppDataContext
    {
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<CommentModel> comments { get; set; }
        IDbSet<GroupModel> groups { get; set; }
        IDbSet<InterestModel> interests { get; set; }
        IDbSet<ThreadModel> threads { get; set; }
        IDbSet<UserInterestUnion> userInterestUnion { get; set; }
        IDbSet<CommentLikeUnion> commentLikeUnion { get; set; }
        IDbSet<ThreadLikeUnion> threadLikeUnion { get; set; }
        IDbSet<UserCommentUnion> userCommentUnion { get; set; }
        IDbSet<UserFriendsUnion> userFriendsUnion { get; set; }
        IDbSet<UserGroupsUnion> userGroupsUnion { get; set; }
        IDbSet<ThreadOriginUnion> threadOriginUnion { get; set; }
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public override IDbSet<ApplicationUser> Users { get; set; }
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


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}