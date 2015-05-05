using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CommentModel
    {
        public int ID { get; set; }
        public string content { get; set; }
        public DateTime dateCreated { get; set; }
        public int likes { get; set; }
        public bool currentUserLiked { get; set; }
    }
}