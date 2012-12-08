using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
        "Post",
        "Archive/{year}/{month}/{title}",
        new { controller = "Blog", action = "Post" }
      );

      routes.MapRoute(
        "Archive",
        "Archive/{year}/{month}",
        new { controller = "Blog", action = "Archive", year = UrlParameter.Optional, month = UrlParameter.Optional }
      );

      routes.MapRoute(
        "Category",
        "Category/{category}",
        new { controller = "Blog", action = "Category" }
      );

      routes.MapRoute(
        "Tag",
        "Tag/{tag}",
        new { controller = "Blog", action = "Tag" }
      );

      routes.MapRoute(
        "Action",
        "{action}",
        new { controller = "Blog", action = "Posts" }
      );

      routes.MapRoute(
        "ControllerAction",
        "{controller}/{action}",
        new { controller = "Admin", action = "Login" }
      );
    }
  }
}