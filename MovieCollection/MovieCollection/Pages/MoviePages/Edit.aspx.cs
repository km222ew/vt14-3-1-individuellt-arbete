using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieCollection.Model;

namespace MovieCollection.Pages.MoviePages
{
    public partial class Edit : System.Web.UI.Page
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
                if (RouteData.Values["id"] != null)
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

        public void MovieFormView_UpdateItem(int MovieID)
        {
            try
            {
                var movie = Service.GetMovie(MovieID);
                if (movie == null)
                {
                    Page.ModelState.AddModelError(String.Empty,
                        String.Format("Movie with id {0} was not found", MovieID));
                    return;
                }

                if (TryUpdateModel(movie))
                {
                    Service.UpdateMovie(movie);

                    MessageStatus = "The movie was updated successfully.";
                    Response.RedirectToRoute("MovieDetails", new { id = movie.MovieID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "An error occured when trying to update the movie.");
            }
        }

        public IEnumerable<Role> RoleListView_GetData()
        {
            try
            {
                return Service.GetRolesByMovieID(GetMovieID);
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "An error occured when trying to obtain a movies roles.");
                return null;
            }
            
        }

        public void RoleListView_UpdateItem(int RoleID)
        {
            try
            {
                var role = Service.GetRole(RoleID);
                if (role == null)
                {
                    Page.ModelState.AddModelError(String.Empty,
                        String.Format("Role with id {0} was not found", RoleID));
                    return;
                }

                if (TryUpdateModel(role))
                {
                    Service.SaveRole(role);

                    MessageStatus = "The role was updated successfully.";
                    Response.RedirectToRoute("MovieEdit", new { id = role.MovieID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "An error occured when trying to update the role.");
            }
        }

        public void RoleListView_DeleteItem(int RoleID)
        {
            try
            {
                Service.DeleteRole(RoleID);

                MessageStatus = "The role was deleted successfully.";
                Response.RedirectToRoute("MovieEdit", new { id = GetMovieID });
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete a role.");
            }

        }

        public void RoleListView_InsertItem(Role role)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    role.MovieID = GetMovieID;
                    Service.SaveRole(role);

                    MessageStatus = "The role was added successfully.";
                    Response.RedirectToRoute("MovieEdit", new { id = GetMovieID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "An error occured when trying to add a role.");
                }
            }
        }

        //protected void RoleListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    var label = e.Item.FindControl("PersonLabel") as Label;
        //    if (label != null)
        //    {
        //        var role = (Role)e.Item.DataItem;

        //        var person = Service.GetPersons().Single(pers => pers.PersID == role.PersID);

        //        label.Text = String.Format(label.Text, person.FullName);
        //    }
        //}

        public IEnumerable<Person> PersonDropDownList_GetData()
        {
            try
            {
                return Service.GetPersons();
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "An error occured when trying to obtain a list of persons.");
                return null;
            }
            
        }
    }
}