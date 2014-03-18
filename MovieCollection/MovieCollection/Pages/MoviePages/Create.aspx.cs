using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCollection.Model;

namespace MovieCollection.Pages.MoviePages
{
    public partial class Create : System.Web.UI.Page
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
            
        }

        public void MovieFormView_InsertItem(Movie movie, Role role)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    Service.InsertMovie(movie, role);

                    MessageStatus = "The movie was added successfully.";
                    Response.RedirectToRoute("Movies");
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error occured when trying to add a movie.");
                }
            }
        }

        public IEnumerable<Person> PersonDropDownList_GetData()
        {
            return Service.GetPersons();
        }
    }
}