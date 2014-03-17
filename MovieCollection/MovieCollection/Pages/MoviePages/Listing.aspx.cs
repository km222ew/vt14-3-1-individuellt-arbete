using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCollection.Model;

namespace MovieCollection.Pages.MoviePages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Movie> MovieListView_GetDataPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetMoviesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void MovieListView_DeleteItem(int MovieID)
        {
            try
            {
                Service.DeleteMovie(MovieID);               
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete a movie.");
            }
        }
    }
}