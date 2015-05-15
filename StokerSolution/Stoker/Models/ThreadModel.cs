using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    public class ThreadModel
    {
        [Key]
        public int threadID { get; set; }
        [Required]
        public string title { get; set; }
        public string mainContent { get; set; }
        public byte[] image { get; set; }
        [Required]
        public DateTime dateCreated { get; set; }
        [Required]
        public int likes { get; set; }
        [Required]
        public virtual ApplicationUser originalPoster { get; set; }
        public virtual ICollection<ApplicationUser> usersLiked { get; set; }
        public virtual ICollection<CommentModel> comments { get; set; }
        public virtual GroupModel groupPost { get; set; }
        public virtual InterestModel interestPost { get; set; }
        public virtual ApplicationUser profilePost {get; set;}
    }
}
