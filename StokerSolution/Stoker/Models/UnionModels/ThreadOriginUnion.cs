using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stoker.Models.UnionModels
{
    public class ThreadOriginUnion
    {
        [Key]
        public int ThreadOriginID { get; set; }

        public ThreadModel Thread { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual InterestModel interest { get; set; }

        public virtual GroupModel Group { get; set; }
    }
}