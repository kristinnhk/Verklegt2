using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CommentModel
    {
        [Key]
        public int commentID { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public DateTime dateCreated { get; set; }
        [Required]
        public int likes { get; set; }
		public virtual ApplicationUser commentAuthor { get; set; }
        public virtual ThreadModel thread { get; set; }
        public virtual ICollection<ApplicationUser> usersLiked { get; set; }
	}
}