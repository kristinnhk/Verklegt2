﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// A template for how information about groups are stored in the database
    /// </summary>
    public class GroupModel
    {
        [Key]
        public int groupID { get; set; }
        [Required]
        public string title { get; set; }
        public string about { get; set; }
        public byte[] image { get; set; }
        public virtual ICollection<ApplicationUser> users { get; set; }
        public virtual ICollection<ThreadModel> threads { get; set; }
        [Required]
        public int numberOfGroupMembers { get; set; }
        public ApplicationUser groupAdmin { get; set; }

    }
}