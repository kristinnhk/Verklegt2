using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class UserGroupsUnion
    {
        [Key]
        public int UserGroupsID { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int GroupModelID { get; set; }
        [ForeignKey("GroupModelID")]
        public GroupModel group { get; set; }
    }
}