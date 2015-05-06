using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// A class to carry information to the views
    /// 
    /// </summary>
    public class ViewModel
    {
        // list of user model ??
        public List<GroupModel> groups { get; set; }
        public List<InterestModel> interests { get; set; }
        public List<ThreadModel> threads { get; set; }
        public List<CommentModel> comments { get; set; }

    }
}