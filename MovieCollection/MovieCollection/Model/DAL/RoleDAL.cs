﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieCollection.Model.DAL
{
    /// <summary>
    /// CRUD-funktionalitet mot tabellen Role
    /// </summary>
    public class RoleDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla roller till en film beroende på "movieID"
        /// </summary>
        /// <returns>En lista med roller</returns>
        public List<Role> GetRolesByMovieID(int movieID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetRolesByMovieID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = movieID;

                    List<Role> roles = new List<Role>(3);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var movieIDIndex = reader.GetOrdinal("MovieID");
                        var roleIDIndex = reader.GetOrdinal("RoleID");
                        var roleIndex = reader.GetOrdinal("Role");
                        var persIDIndex = reader.GetOrdinal("PersID");

                        while (reader.Read())
                        {
                            roles.Add(new Role
                            {
                                MovieID = reader.GetInt32(movieIDIndex),
                                RoleID = reader.GetInt32(roleIDIndex),
                                MovieRole = reader.GetString(roleIndex),
                                PersID = reader.GetInt32(persIDIndex)
                            });
                        }
                    }

                    roles.TrimExcess();

                    return roles;
                }
                catch (Exception)
                {

                    throw new ApplicationException("An error has occured when trying to get roles from the database.");
                }
            }
        }

        /// <summary>
        /// Hämtar en roll beroende på "roleID"
        /// </summary>
        /// <returns>En roll</returns>
        public Role GetRole(int roleID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var movieIDIndex = reader.GetOrdinal("MovieID");
                            var roleIDIndex = reader.GetOrdinal("RoleID");
                            var roleIndex = reader.GetOrdinal("Role");
                            var persIDIndex = reader.GetOrdinal("PersID");
 
                            return new Role
                            {
                                MovieID = reader.GetInt32(movieIDIndex),
                                RoleID = reader.GetInt32(roleIDIndex),
                                MovieRole = reader.GetString(roleIndex),
                                PersID = reader.GetInt32(persIDIndex)
                            };
                        }
                    }
                    return null;
                }
                catch (Exception)
                {

                    throw new ApplicationException("An error has occured when trying to get a role from the database.");
                }
            }
        }

        /// <summary>
        /// Lägger till en roll för en film
        /// </summary>
        public void InsertRole(Role role)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_NewRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = role.MovieID;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 60).Value = role.MovieRole;
                    cmd.Parameters.Add("@PersID", SqlDbType.Int, 4).Value = role.PersID;

                    //Hämtar det nya id:t som skapats efter proceduren har körts
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    role.RoleID = (int)cmd.Parameters["@RoleID"].Value;

                }
                catch (Exception)
                {

                    throw new ApplicationException("An error has occured when trying to add a role.");
                }
            }
        }

        /// <summary>
        /// Uppdaterar en roll
        /// </summary>
        public void UpdateRole(Role role)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_UpdateRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = role.MovieID;
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = role.RoleID;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 60).Value = role.MovieRole;
                    cmd.Parameters.Add("@PersID", SqlDbType.Int, 4).Value = role.PersID;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to update a role.");
                }
            }
        }

        /// <summary>
        /// Tar bort en roll beroende på "roleID"
        /// </summary>
        public void DeleteRole(int roleID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_DeleteRole", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to delete a role.");
                }
            }
        }
    }
}