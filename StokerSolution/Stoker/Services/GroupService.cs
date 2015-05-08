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

        public void SetGroup(GroupModel newGroup)
        {
            try
            {
                db.groups.Add(newGroup);
                db.SaveChanges();
                return;
            }catch
            {
                return;
            }
        }
        
        public GroupModel GetGroupByID(int groupID)
        {
            return (from selectGroup in db.groups
                    where selectGroup.groupID == groupID
                    select selectGroup).FirstOrDefault();
        }


    }
}