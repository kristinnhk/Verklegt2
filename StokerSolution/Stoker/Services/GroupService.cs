using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Stoker.Models;

namespace Stoker.Services
{
    public class GroupService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Adds a new group to the databaser
        /// </summary>
        /// <param name="newGroup">A model of the group to be added</param>
        public void SetGroup(GroupModel newGroup)
        {
            try
            {
                db.groups.Add(newGroup);
                db.SaveChanges();
                return;
            }
            catch
            {
                return;
            }
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
                    select selectGroup).FirstOrDefault();
        }

        /// <summary>
        /// This function returns a list of users who are interested in the interest of the given id
        /// Dude to how the union tables work, the FK's are directly pointing to the model itself
        /// The FK names are a bit off though, since the entity framework made them for us using code first
        /// </summary>
        /// <param name="interestID">the id of the interest we are looking to find the users for</param>
        /// <returns></returns>
        /*public IEnumerable<ApplicationUser> GetGroupUsers(int ID)
        {
            
            IEnumerable<ApplicationUser> users = (from u in db.userGroupsUnion
                                                  where u. == ID
                                                  select u.User);

            return users;
        }*/
    }
}