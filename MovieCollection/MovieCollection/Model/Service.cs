using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MovieCollection.Model.DAL;

namespace MovieCollection.Model
{
    public class Service
    {
        private MovieDAL _movieDAL;
        private PersonDAL _personDAL;
        private RoleDAL _roleDAL;

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

        public IEnumerable<Movie> GetMoviesPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return MovieDAL.GetMoviesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public Movie GetMovie(int movieID)
        {
            return MovieDAL.GetMovie(movieID);
        }

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

        public void DeleteMovie(int movieID)
        {
            MovieDAL.DeleteMovie(movieID);
        }

        public List<Role> GetRolesByMovieID(int movieID)
        {
            return RoleDAL.GetRolesByMovieID(movieID);
        }

        public Role GetRole(int roleID)
        {
            return RoleDAL.GetRole(roleID);
        }

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

        public void DeleteRole(int roleID)
        {
            RoleDAL.DeleteRole(roleID);
        }

        public IEnumerable<Person> GetPersons(bool refresh = false)
        {
            var persons = HttpContext.Current.Cache["Persons"] as IEnumerable<Person>;

            if (persons == null || refresh)
            {
                persons = PersonDAL.GetPersons();

                HttpContext.Current.Cache.Insert("Persons", persons, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }

            return persons;
        }
    }
}