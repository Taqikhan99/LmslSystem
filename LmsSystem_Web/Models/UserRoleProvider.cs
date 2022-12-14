using LmsSystem_DAL.Concrete;
using LmsSystem_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LmsSystem_Web.Models
{
    public class UserRoleProvider : RoleProvider
    {
        DbClass db = new DbClass();

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
        //Get user roles
        public override string[] GetRolesForUser(string email)
        {
            string[] roles= new string[1];
            SqlConnection con =db.GetConnection();
            SqlCommand cmd = new SqlCommand("spGetUserRoles", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userEmail", email);
            //getting data in dataadapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            //return role name and add to roles array
            DataRow dataRow = dt.Rows[0];
            roles[0] = dataRow["RoleName"].ToString();

            return roles;
            
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