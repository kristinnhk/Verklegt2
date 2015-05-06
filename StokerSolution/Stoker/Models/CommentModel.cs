using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CommentModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public DateTime dateCreated { get; set; }
        [Required]
        public int likes { get; set; }
        [Required]
        public bool currentUserLiked { get; set; }
    }
}