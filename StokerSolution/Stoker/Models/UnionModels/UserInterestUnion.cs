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

        public virtual ApplicationUser User { get; set; }

        public virtual InterestModel interestID { get; set; }
    }
}