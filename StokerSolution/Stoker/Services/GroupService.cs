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
                UserService service = new UserService(db);
                if (newGroup.groupAdmin.groups == null)
                {
                    newGroup.groupAdmin.groups = new List<GroupModel>();
                }
                newGroup.groupAdmin.groups.Add(newGroup);
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

            GroupModel group = GetGroupByID(groupID);
               if(group == null)
               {
                   return;
               } 
                   group.title = title;
                db.SaveChanges();
        }

        /// <summary>
        /// Changes the about section of an existing group
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <param name="about">New about section of the group</param>
        public void SetGroupAbout(int groupID, string about)
        {
                
            GroupModel group = GetGroupByID(groupID);
            if(group == null)
            {
                return;
            }
                    group.about = about;
                db.SaveChanges();
        }

        /// <summary>
        /// Changes the number of groupmembers
        /// </summary>
        /// <param name="groupID">ID of the group</param>
        /// <param name="members">New number of members of the group</param>
        public void SetNumberOfGroupMembers(int groupID, int members)
        {
                GetGroupByID(groupID).numberOfGroupMembers = members;
                db.SaveChanges();
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

        /// <summary>
        /// Sets a default image for a group if none has been selected
        /// </summary>
        /// <param name="group">the group model</param>
        public void SetImageDefault(GroupModel group)
        {
            string pathPrefix = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images"), "default-group.jpg");
            System.Drawing.Image imageIn = System.Drawing.Image.FromFile(pathPrefix);
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            group.image = ms.ToArray();
 
        }

        /// <summary>
        /// deletes a user from a group
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="groupID">id of the interest</param>
        public void DeleteUserGroup(string userID, int groupID)
        {
            UserService serviceUser = new UserService(db);
            ApplicationUser user = serviceUser.GetUserByID(userID);
            GroupModel group = GetGroupByID(groupID);
            user.groups.Remove(group);
            group.users.Remove(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Returns true if user is a member of a group
        /// but false otherwise
        /// </summary>
        /// <param name="userID">id of the user</param>
        /// <param name="groupID">id of the group</param>
        /// <returns>Returns true if user is following an interest
        /// but false otherwise</returns>
        public bool IsMemberOfGroup(string userID, int groupID)
        {
            GroupModel groups = GetGroupByID(groupID);
            ApplicationUser following = (from u in groups.users
                                         where u.Id == userID
                                         select u).SingleOrDefault();
            if (following == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}