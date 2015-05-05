using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadModel
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string mainContent { get; set; }
        public byte[] image { get; set; }
        public DateTime dateCreated { get; set; }
        public int likes { get; set; }
        public bool currentUserLiked { get; set; }
    }
}