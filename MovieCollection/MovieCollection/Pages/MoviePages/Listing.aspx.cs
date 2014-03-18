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

        private string MessageStatus
        {
            get { return Session["MessageStatus"] as string; }
            set { Session["MessageStatus"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MessageStatus != null)
            {
                successPanel.Visible = true;
                successLabel.Text = MessageStatus;
                Session.Clear();
            }
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

                MessageStatus = "The movie was deleted successfully.";
                Response.RedirectToRoute("Movies");
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete a movie.");
            }
        }
    }
}