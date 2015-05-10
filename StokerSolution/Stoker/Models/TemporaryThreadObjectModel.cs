using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using System.Security.Principal;
using Stoker.Models;
namespace Stoker.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class TemporaryThreadObjectModel
	{
		public int threadID;
		public IPrincipal user;
		public string title;
		public InterestModel interest;
		public string mainContent;
		/*image*/
		public List<CommentModel> comments;
		public DateTime dateCreated;
		public int likes;
		public bool currentUserLiked;
	}
}
