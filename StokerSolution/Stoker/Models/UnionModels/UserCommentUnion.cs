using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class UserCommentUnion
    {
        [Key]
        public int UserCommentID { get; set; }

        public virtual CommentModel comment { get; set; }

        public virtual ThreadModel Thread { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}