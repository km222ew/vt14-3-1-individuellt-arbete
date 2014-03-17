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

                    //Parametrar
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

        /// <summary>
        /// Hämtar en film beroende "movieID"
        /// </summary>
        /// <returns>Ett film-objekt</returns>
        public Movie GetMovie(int movieID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_GetMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = movieID;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var movieIDIndex = reader.GetOrdinal("MovieID");
                        var titleIndex = reader.GetOrdinal("Title");
                        var storyIndex = reader.GetOrdinal("Story");

                        if (reader.Read())
                        {
                            return new Movie
                            {
                                MovieID = reader.GetInt32(movieIDIndex),
                                Title = reader.GetString(titleIndex),
                                Story = reader.GetString(storyIndex)
                            };
                        }
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to get a movie from the database.");
                }
            }
        }

        public void InsertMovie(Movie movie, Role role)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_NewMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 60).Value = movie.Title;
                    cmd.Parameters.Add("@Story", SqlDbType.VarChar, 1000).Value = movie.Story;
                    cmd.Parameters.Add("@Role", SqlDbType.VarChar, 60).Value = role.MovieRole ;
                    cmd.Parameters.Add("@PersID", SqlDbType.Int, 4).Value = role.PersID;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    movie.MovieID = (int)cmd.Parameters["@MovieID"].Value;
                    role.RoleID = (int)cmd.Parameters["@RoleID"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to add a movie.");
                }
            }
        }

        public void UpdateMovie(Movie movie)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_UpdateMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = movie.MovieID;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar, 60).Value = movie.Title;
                    cmd.Parameters.Add("@Story", SqlDbType.VarChar, 1000).Value = movie.Story;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to update a movie.");
                }
            }
        }

        public void DeleteMovie(int movieID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("appSchema.usp_DeleteMovie", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MovieID", SqlDbType.Int, 4).Value = movieID;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException("An error has occured when trying to delete a movie.");
                }
            }
        }
    }
}