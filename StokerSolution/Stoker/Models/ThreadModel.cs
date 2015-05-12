using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadModel
    {
        [Key]
        public int threadID { get; set; }
        [Required]
        public string title { get; set; }
        public string mainContent { get; set; }
        public byte[] image { get; set; }
        [Required]
        public DateTime dateCreated { get; set; }
        [Required]
        public int likes { get; set; }
        [Required]
        public string nameOfPoster { get; set; }
        [Required]
        public bool currentUserLiked { get; set; }
    }
}
