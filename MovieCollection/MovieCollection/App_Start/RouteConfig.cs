using System.Web.Routing;

namespace MovieCollection
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Movies", "movies", "~/Pages/MoviePages/Listing.aspx");
            routes.MapPageRoute("MovieCreate", "movies/new", "~/Pages/MoviePages/Create.aspx");
            routes.MapPageRoute("MovieDetails", "movies/{id}", "~/Pages/MoviePages/Details.aspx");
            routes.MapPageRoute("MovieEdit", "movies/{id}/edit", "~/Pages/MoviePages/Edit.aspx");
            routes.MapPageRoute("MovieDelete", "movies/{id}/delete", "~/Pages/MoviePages/Delete.aspx");

            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/Error.aspx");

            routes.MapPageRoute("Index", "", "~/Pages/MoviePages/Index.aspx");
        }
    }
}