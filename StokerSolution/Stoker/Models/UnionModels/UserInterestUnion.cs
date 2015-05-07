using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class UserInterestUnion
    {
        [Key]
        public int userInterestID { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int InterestModelID { get; set; }
        [ForeignKey("InterestModelID")]
        public InterestModel interest { get; set; }
    }
}