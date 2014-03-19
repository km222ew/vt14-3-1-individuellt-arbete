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

        //Skapar ett service-objekt när det behövs
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        //Inkapslad session för (rätt)meddelande
        private string MessageStatus
        {
            get { return Session["MessageStatus"] as string; }
            set { Session["MessageStatus"] = value; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Kollar om sessionen inte är null, visar i så fall en label med rättmeddelandet som text och tömmer sessionen
            if (MessageStatus != null)
            {
                successPanel.Visible = true;
                successLabel.Text = MessageStatus;
                Session.Clear();
            }
        }

        //Hämtar filmer 20 per sida
        public IEnumerable<Movie> MovieListView_GetDataPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetMoviesPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        //Tar bort en film
        public void MovieListView_DeleteItem(int MovieID)
        {
            try
            {
                Service.DeleteMovie(MovieID);

                //Rättmeddelande
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