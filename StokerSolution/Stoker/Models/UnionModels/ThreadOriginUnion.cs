using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class ThreadOriginUnion
    {
        [Key]
        public int ThreadOriginID { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int InterestModelID { get; set; }
        [ForeignKey("InterestModelID")]
        public InterestModel interest { get; set; }

        public int GroupModelID { get; set; }
        [ForeignKey("GroupModelID")]
        public GroupModel group { get; set; }
    }
}