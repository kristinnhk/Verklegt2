using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stoker.Models
{
	public class SidebarModel
	{
		public List<GroupModel> userGroups { get; set; }
		public List<InterestModel> userInterests { get; set; }
	}
}