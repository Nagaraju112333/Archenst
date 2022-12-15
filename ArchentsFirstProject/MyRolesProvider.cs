using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ArchentsFirstProject.Models;

namespace ArchentsFirstProject
{
    public class MyRolesProvider : RoleProvider
    {
        private ArchentsEntities7 db = new ArchentsEntities7();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        /*  public  Register GetAllRoles( int id ,int role)
          {
              var result = db.Registers.FirstOrDefault(x => x.RegisterId ==id &&x.RoleType==role);
             return result;
          }
  */
        public override string[] GetRolesForUser(string id)
        {
            var obj = db.Registers.FirstOrDefault(x => x.RoleType == Convert.ToInt32(id));
            if (obj != null)
            {
                string role = obj.RoleType.ToString();
                string[] roleAry = { role };
                return roleAry;
            }
            else
            {
                throw new NotImplementedException();
            }


        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}