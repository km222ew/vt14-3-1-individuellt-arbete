using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCollection.Model;

namespace MovieCollection.Pages.MoviePages
{
    public partial class Details : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        public int GetMovieID
        {
            get 
            {
                if (RouteData.Values["id"] !=null)
                {
                    return int.Parse(RouteData.Values["id"].ToString());
                }
                else
                {
                    Response.RedirectToRoute("Movies");
                    Context.ApplicationInstance.CompleteRequest();
                    return 0;
                }         
            }
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

        public Movie MovieFormView_GetItem()
        {
            try
            {
                return Service.GetMovie(GetMovieID);
            }
            catch (Exception)
            {               
                Page.ModelState.AddModelError(String.Empty, "An error occured when trying to obtain the movie.");
                return null;
            }
        }

        public void MovieFormView_DeleteItem(int MovieID)
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

        public IEnumerable<Role> RoleListView_GetData()
        {
            return Service.GetRolesByMovieID(GetMovieID);
        }

        protected void RoleListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("PersonLabel") as Label;
            if (label != null)
            {
                var role = (Role)e.Item.DataItem;

                var person = Service.GetPersons().Single(pers => pers.PersID == role.PersID);

                label.Text = String.Format(label.Text, person.FullName);
            }
        }
    }
}