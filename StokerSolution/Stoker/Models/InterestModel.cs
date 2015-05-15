using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// template for how information about interests should be stored
    /// </summary>
    public class InterestModel
    {
        [Key]
        public int interestID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int numberOfUsersInterested { get; set; }
        public virtual ICollection<ApplicationUser> users { get; set; }
        public virtual ICollection<ThreadModel> threads { get; set; }
    }
}