using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Collections.Generic;
using System.Data.Entity.Validation;

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
    
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public override IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<CommentModel> comments { get; set; }
        public IDbSet<GroupModel> groups { get; set; }
        public IDbSet<InterestModel> interests { get; set; }
        public IDbSet<ThreadModel> threads { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /// <summary>
        /// This catches an exception we do not know what causes
        /// and logs its message
        /// </summary>
        /// <returns>a detailed errormessage</returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}