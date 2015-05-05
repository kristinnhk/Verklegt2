using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GroupModel
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public byte[] image { get; set; }
        public int numberOfGroupMembers { get; set; }
    }
}