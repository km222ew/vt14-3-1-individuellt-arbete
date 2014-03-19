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

        //Skapar ett service-objekt när det behövs
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        //Hämtar rätt "MovieID" från url:en
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

        //Hämtar filmen som ska uppdateras
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

        //Uppdaterar filmen
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

                    //Rättmeddelande
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

        //Hämtar alla roller för filmen
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

        //Uppdaterar en roll
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

                    //Rättmeddelande
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

        //Tar bort en roll
        public void RoleListView_DeleteItem(int RoleID)
        {
            try
            {
                Service.DeleteRole(RoleID);

                //Rättmeddelande
                MessageStatus = "The role was deleted successfully.";

                Response.RedirectToRoute("MovieEdit", new { id = GetMovieID });
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "An error occured when trying to delete a role.");
            }

        }

        //Lägger till en ny roll
        public void RoleListView_InsertItem(Role role)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    role.MovieID = GetMovieID;
                    Service.SaveRole(role);

                    //Rättmeddelande
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

        //Hämtar alla personerna och lägger dem i dropdown-listan
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