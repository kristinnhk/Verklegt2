using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
    /// <summary>
    /// used to render information in the sidebar of the site
    /// </summary>
	public class SidebarModel
	{
		public List<GroupModel> userGroups { get; set; }
		public List<InterestModel> userInterests { get; set; }
	}
}