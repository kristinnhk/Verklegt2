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

        public ApplicationUser User { get; set; }

        public InterestModel interest { get; set; }

        public GroupModel group { get; set; }
    }
}