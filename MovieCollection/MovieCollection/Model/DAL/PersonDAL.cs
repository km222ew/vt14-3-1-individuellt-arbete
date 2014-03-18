using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieCollection.Model.DAL
{
    /// <summary>
    /// (C)R(UD)-funktionalitet mot tabellen Person
    /// </summary>
    public class PersonDAL : DALBase
    {
        /// <summary>
        /// Hämtar alla personer i tabellen
        /// </summary>
        /// <returns>En lista med personer</returns>
        public IEnumerable<Person> GetPersons()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetPersons", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<Person> persons = new List<Person>(50);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var personIDIndex = reader.GetOrdinal("PersID");
                        var firstNameIndex = reader.GetOrdinal("Firstname");
                        var lastNameIndex = reader.GetOrdinal("Lastname");
                        var fullNameIndex = reader.GetOrdinal("FullName");

                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                PersID = reader.GetInt32(personIDIndex),
                                Firstname = reader.GetString(firstNameIndex),
                                Lastname = reader.GetString(lastNameIndex),
                                FullName = reader.GetString(fullNameIndex)
                            });
                        }
                    }

                    persons.TrimExcess();

                    return persons;
                }
                catch (Exception)
                {

                    throw new ApplicationException("An error has occured when trying to get persons from the database.");
                }
            }
        }
    }
}