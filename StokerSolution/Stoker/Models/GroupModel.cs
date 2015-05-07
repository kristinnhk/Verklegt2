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
    public class GroupModel
    {
        [Key]
        public int groupID { get; set; }
        [Required]
        public string title { get; set; }
        public string about { get; set; }
        public byte[] image { get; set; }
        [Required]
        public int numberOfGroupMembers { get; set; }
    }
}