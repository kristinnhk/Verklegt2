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

        public int CommentModelID { get; set; }
        [ForeignKey("CommentModelID")]
        public CommentModel comment { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}