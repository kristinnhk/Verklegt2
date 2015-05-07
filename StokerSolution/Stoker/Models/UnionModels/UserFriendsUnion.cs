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

        public ApplicationUser User { get; set; }

        public ApplicationUser User2 { get; set; }

    }
}