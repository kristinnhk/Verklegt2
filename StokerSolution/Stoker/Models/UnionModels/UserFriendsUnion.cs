using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class UserFriendsUnion
    {
        [Key]
        public int UserFriendsID { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string UserId2 { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User2 { get; set; }

    }
}