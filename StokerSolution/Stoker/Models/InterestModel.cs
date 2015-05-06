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
    public class InterestModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int numberOfUsersInterested { get; set; }

    }
}