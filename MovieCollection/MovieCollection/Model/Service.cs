using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MovieCollection.Model.DAL;

namespace MovieCollection.Model
{
    /// <summary>
    /// Klassen som presentationslogiklagret anropar för att hantera data
    /// som sedan anropar dataåtkomstlagrets klasser.
    /// </summary>
    public class Service
    {
        #region Fält
        //Fält
        private MovieDAL _movieDAL;
        private PersonDAL _personDAL;
        private RoleDAL _roleDAL;
        #endregion

        #region Egenskaper
        //Egenskaper
        private MovieDAL MovieDAL
        {
            get { return _movieDAL ?? (_movieDAL = new MovieDAL()); }
        }

        private PersonDAL PersonDAL
        {
            get { return _personDAL ?? (_personDAL = new PersonDAL()); }
        }

        private RoleDAL RoleDAL
        {
            get { return _roleDAL ?? (_roleDAL = new RoleDAL()); }
        }
        #endregion

        #region Movie CRUD-Metoder

        //Hämtar filmer sidvis
        public IEnumerable<Movie> GetMoviesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return MovieDAL.GetMoviesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        //Hämtar en film med "movieID"
        public Movie GetMovie(int movieID)
        {
            return MovieDAL.GetMovie(movieID);
        }

        //Skapar en ny film i databasen
        public void InsertMovie(Movie movie, Role role)
        {
            ICollection<ValidationResult> validationResults;
            if (!movie.Validate(out validationResults) && !role.Validate(out validationResults))
            {
                var ex = new ValidationException("The object did not pass the validation.");
                ex.Data.Add("ValidationResult", validationResults);
                throw ex;
            }
                
            MovieDAL.InsertMovie(movie, role);
        }

        //Uppdaterar en film
        public void UpdateMovie(Movie movie)
        {
            ICollection<ValidationResult> validationResults;
            if (!movie.Validate(out validationResults))
            {
                var ex = new ValidationException("The object did not pass the validation.");
                ex.Data.Add("ValidationResult", validationResults);
                throw ex;
            }

            MovieDAL.UpdateMovie(movie);
        }

        //Tar bort en film med "movieID"
        public void DeleteMovie(int movieID)
        {
            MovieDAL.DeleteMovie(movieID);
        }
        #endregion

        #region Role CRUD-Metoder

        //Hämtar roller med "movieID"
        public List<Role> GetRolesByMovieID(int movieID)
        {
            return RoleDAL.GetRolesByMovieID(movieID);
        }

        //Hämtar en roll med "roleID"
        public Role GetRole(int roleID)
        {
            return RoleDAL.GetRole(roleID);
        }

        //Skapar en ny roll i databasen eller uppdaterar en befintlig
        public void SaveRole(Role role)
        {
            ICollection<ValidationResult> validationResults;
            if (!role.Validate(out validationResults))
            {
                var ex = new ValidationException("The object did not pass the validation.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }
            if (role.RoleID == 0)
            {
                RoleDAL.InsertRole(role);
            }
            else
            {
                RoleDAL.UpdateRole(role);
            }
        }

        //Tar bort en roll med "roleID"
        public void DeleteRole(int roleID)
        {
            RoleDAL.DeleteRole(roleID);
        }
        #endregion

        #region Person (C)R(UD)-Metod

        //Hämtar alla personer
        public IEnumerable<Person> GetPersons()
        {
            //Om det finns hämtas en lista med personer från cache
            var persons = HttpContext.Current.Cache["Persons"] as IEnumerable<Person>;

            //Om det inte fanns...
            if (persons == null)
            {
                //...så hämtas en ny lista med personer...
                persons = PersonDAL.GetPersons();

                //... som cachas i 10 minuter.
                HttpContext.Current.Cache.Insert("Persons", persons, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            //Listan returnera i vilket fall
            return persons;
        }

        #endregion
    }
}