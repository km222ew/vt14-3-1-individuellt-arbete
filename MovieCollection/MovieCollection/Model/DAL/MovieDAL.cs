using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieCollection.Model.DAL
{
    /// <summary>
    /// CRUD-funktionalitet mot tabellen Movie
    /// </summary>
    public class MovieDAL : DALBase
    {
        /// <summary>
        /// Hämtar filmer sid-vis istället för alla på en gång 
        /// </summary>
        /// <returns>En lista med filmer</returns>
        public IEnumerable<Movie> GetMoviesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //En lista skapas med plats för så många filmer som maximumRows anger
                    var movies = new List<Movie>(maximumRows);

                    var cmd = new SqlCommand("appSchema.usp_GetMoviesPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Parametrar som behövs i den lagrade proceduren
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var movieIDIndex = reader.GetOrdinal("MovieID");
                        var titleIndex = reader.GetOrdinal("Title");
                        var storyIndex = reader.GetOrdinal("Story");

                        while (reader.Read())
                        {
                            movies.Add(new Movie
                            {
                                MovieID = reader.GetInt32(movieIDIndex),
                                Title = reader.GetString(titleIndex),
                                Story = reader.GetString(storyIndex)
                            });
                        }
                    }

                    movies.TrimExcess();

                    return movies;
                }
                catch (Exception)
                {

                    throw new ApplicationException("An error has occured when trying to get movies page-wise from the database.");
                }
            }
        }
    }
}