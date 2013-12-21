using System;
using System.Linq;
using System.Configuration;
using System.Web.Mvc;

namespace JustBlog
{
  public class ThemedRazorViewEngine: RazorViewEngine
  {
    public ThemedRazorViewEngine(string theme)
    {
      var additionalViewLocations = new[]{
        String.Format("~/Assets/themes/{0}/html/{{1}}/{{0}}.cshtml", theme),
        String.Format("~/Assets/themes/{0}/html/Shared/{{0}}.cshtml", theme)
      };

      ViewLocationFormats = additionalViewLocations.Union(ViewLocationFormats).ToArray();
      MasterLocationFormats = additionalViewLocations.Union(MasterLocationFormats).ToArray();
      PartialViewLocationFormats = additionalViewLocations.Union(PartialViewLocationFormats).ToArray();
    }    
  }
}