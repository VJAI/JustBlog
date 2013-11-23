using System.Web.Mvc;

namespace JustBlog.Controllers
{
  /// <summary>
  /// Contain actions to return views for different errors.
  /// </summary>
  public class ErrorController : Controller
  {
    /// <summary>
    /// Return view for generic errors (for ex. Internal Server Error like 500).
    /// </summary>
    /// <returns></returns>
    public ActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Return view for 404 errors.
    /// </summary>
    /// <returns></returns>
    public ActionResult NotFound()
    {
      return View();
    }
  }
}
