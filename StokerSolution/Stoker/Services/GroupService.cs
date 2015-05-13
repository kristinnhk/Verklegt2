using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;
using System.IO;

namespace Stoker.Services
{
    public class GroupService
    {
        private readonly IAppDataContext db;

        public GroupService(IAppDataContext context = null)
        {
            db = context ?? new ApplicationDbContext();
        }

        /// <summary>
        /// Adds a new group to the databaser
        /// </summary>
        /// <param name="newGroup">A model of the group to be added</param>
        public void SetGroup(GroupModel newGroup)
        {
            if (newGroup.users == null)
            {
                newGroup.users = new List<ApplicationUser>();
            }
                if (newGroup.image == null)
                {
                    SetImageDefault(newGroup);
                }
                if (newGroup.threads == null)
                {
                    newGroup.threads = new List<ThreadModel>();
                }
                db.groups.Add(newGroup);
                db.SaveChanges();
                return;
        }

        /// <summary>
        /// Changes the title of an existing group
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <param name="title">New title of the group</param>
        public void SetGroupTitle(int groupID, string title)
        {
            try
            {
                GetGroupByID(groupID).title = title;
                db.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes the about section of an existing group
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <param name="about">New about section of the group</param>
        public void SetGroupAbout(int groupID, string about)
        {
            try
            {
                GetGroupByID(groupID).about = about;
                db.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Changes the number of groupmembers
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <param name="members">New number of members of the group</param>
        public void SetGroupMembers(int groupID, int members)
        {
            try
            {
                GetGroupByID(groupID).numberOfGroupMembers = members;
                db.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// This function Gets groups from the database by searching the title
        /// field sorted alphabetically.
        /// </summary>
        /// <param name="title">title is the string searched for</param>
        /// <returns>this function returns a list of GroupModels
        /// sorted in alphabetical order.</returns>
        public IEnumerable<GroupModel> GetGroupByTitle(string title)
        {
            IEnumerable<GroupModel> groupList = (from groupTitle in db.groups
                                                 where groupTitle.title.Contains(title)
                                                 select groupTitle)
                                                     ;
            IEnumerable<GroupModel> groupReturnList = groupList.OrderBy(groups => groups.title);
            return groupReturnList;
        }

        /// <summary>
        /// Gets a group from the database by it's ID
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <returns>returns a GroupModel of the group.</returns>
        public GroupModel GetGroupByID(int groupID)
        {
            return (from selectGroup in db.groups
                    where selectGroup.groupID == groupID
                    select selectGroup).SingleOrDefault();
        }

        /// <summary>
        /// This function returns a list of users of the group in the group with the given id
        /// </summary>
        /// <param name="ID">the id of the group we are looking to find the users for</param>
        /// <returns>returns a collection of users of the group if the group
        ///  exists, if not it returns null</returns>
        public ICollection<ApplicationUser> GetGroupUsers(int ID)
        {
            GroupModel group = GetGroupByID(ID);
            if (group != null)
            {
                return group.users;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the groups a user is a member of
        /// </summary>
        /// <param name="userID">ID of the user</param>
        /// <returns>List of groups the user is a member of if the group exists if not returns null</returns>
        public ICollection<GroupModel> GetUserGroups(string userID)
        {
            UserService service = new UserService(db);

            ApplicationUser user = service.GetUserByID(userID);
            if (user != null)
            {
                return user.groups;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a relation between a user and a group.
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="groupID">id of the group</param>
        public void SetUserGroup(string userID, int groupID)
        {
            UserService service = new UserService(db);

            ApplicationUser user = service.GetUserByID(userID);
            //If the user does not exist nothing will be changed.
            if (user == null)
            {
                return;
            }
            else if (user.groups == null)
            {
                user.groups = new List<GroupModel>();
            }
            GroupModel group = GetGroupByID(groupID);
            //if the group does not exist nothing will be changed.
            if (group == null)
            {
                return;
            }
            else if (group.users == null)
            {
                group.users = new List<ApplicationUser>();
            }
            group.users.Add(user);
            user.groups.Add(group);
            db.SaveChanges();
        }

        /// <summary>
        /// Changes image of the current group
        /// </summary>
        /// <param name="groupID">current group ID</param>
        /// <param name="image">the new image you want to set</param>
        public void SetImage(int groupID, byte[] image)
        {
            try
            {
                GetGroupByID(groupID).image = image;
                db.SaveChanges();
                return;
            }
            catch
            {
                return;
            }
        }

        public void SetImageDefault(GroupModel group)
        {
            string pathPrefix = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images"), "default-group.jpg");
            System.Drawing.Image imageIn = System.Drawing.Image.FromFile(pathPrefix);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            group.image = ms.ToArray();
 
        }

    }
}