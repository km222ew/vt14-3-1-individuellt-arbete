using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MovieCollection.Model.DAL
{   
        public abstract class DALBase
        {
            // Innehåller anslutnings-strängen för att kunna ansluta till databasen
            private static string _connectionString;

            // Skapar och initierar ett anslutnings-objekt
            protected SqlConnection CreateConnection()
            {
                return new SqlConnection(_connectionString);
            }

            // Konstruktorn som initierar statiskt data
            static DALBase()
            {
                _connectionString = WebConfigurationManager.ConnectionStrings["movieCollectionConnString"].ConnectionString;
            }
        }
    }
