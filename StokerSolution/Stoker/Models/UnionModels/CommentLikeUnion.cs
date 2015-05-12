using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class CommentLikeUnion
    {
        [Key]
        public int CommentLikeID { get; set; }

        public virtual CommentModel comment { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}