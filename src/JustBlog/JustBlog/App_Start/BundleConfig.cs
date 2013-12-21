using System.Web;
using System.Web.Optimization;
using System.Xml.Linq;
using System.Linq;

namespace JustBlog
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles, string theme = "default")
    {
      bundles.UseCdn = true;

      // jquery library bundle
      var jqueryBundle = new ScriptBundle("~/jquery", "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js")
                              .Include("~/Assets/jquery-{version}.js");
      bundles.Add(jqueryBundle);

      // jquery validation library bundle
      var jqueryValBundle = new ScriptBundle("~/jqueryval", "http://ajax.aspnetcdn.com/ajax/jquery.validate/1.10.0/jquery.validate.min.js")
                                  .Include("~/Assets/jquery.validate.js");
      bundles.Add(jqueryValBundle);

      // jquery unobtrusive validation library
      var jqueryUnobtrusiveValBundle = new ScriptBundle("~/jqueryunobtrusiveval", "http://ajax.aspnetcdn.com/ajax/mvc/3.0/jquery.validate.unobtrusive.min.js")
                                          .Include("~/Assets/jquery.validate.unobtrusive.js");
      bundles.Add(jqueryUnobtrusiveValBundle);

      // login page bundles
      var loginCssBundle = new StyleBundle("~/Assets/admin/css/bundle").Include("~/Assets/admin/css/style.css");
      bundles.Add(loginCssBundle);

      // manage page bundles
      var jqueryUIBundle = new ScriptBundle("~/jqueryui", "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.1/jquery-ui.min.js").Include("~/Assets/admin/scripts/jquery-ui.js");
      bundles.Add(jqueryUIBundle);

      var manageCssBundle = new StyleBundle("~/Assets/admin/scripts/jqgrid/css/bundle").Include("~/Assets/admin/scripts/jqgrid/css/ui.jqgrid.css");
      bundles.Add(manageCssBundle);

      var jqueryUICssBundle =
      new StyleBundle("~/Assets/admin/css/jqueryuicustom/css/sunny/bundle").Include("~/Assets/admin/css/jqueryuicustom/css/sunny/jquery-ui-1.9.2.custom.css");
      bundles.Add(jqueryUICssBundle);

      var tinyMceBundle = new ScriptBundle("~/Assets/admin/scripts/tiny_mce/js").Include("~/Assets/admin/scripts/tiny_mce/tiny_mce.js");
      bundles.Add(tinyMceBundle);

      var manageJsBundle = new ScriptBundle("~/manage/js").Include("~/Assets/admin/scripts/jqgrid/js/jquery.jqGrid.js").Include("~/Assets/admin/scripts/jqgrid/js/i18n/grid.locale-en.js").Include("~/Assets/admin/scripts/admin.js");
      bundles.Add(manageJsBundle);

      var themeConfigEl = XElement.Load(HttpContext.Current.Server.MapPath(string.Format("~/App_Data/ThemeConfig/{0}.xml", theme)));

      if(themeConfigEl == null) return;

      var bundleEls = themeConfigEl.Elements();

      foreach (var bundleEl in bundleEls)
      {
        var bundleType = bundleEl.Attribute("Type").Value;
        var virtualPath = bundleEl.Attribute("VirtualPath").Value;
        var cdnPath = bundleEl.Attribute("CdnPath") != null ? bundleEl.Attribute("CdnPath").Value : "";
        var include = bundleEl.Attribute("Include") != null ? bundleEl.Attribute("Include").Value : "";

        var bundle = bundleType == "js" ? (Bundle)new ScriptBundle(virtualPath) : (Bundle)new StyleBundle(virtualPath);

        if (!string.IsNullOrEmpty(cdnPath))
          bundle.CdnPath = cdnPath;

        if (!string.IsNullOrEmpty(include))
        {
          bundle.Include(include);
        }
        else
        {
          var includes = bundleEl.Elements();
          foreach (var i in includes)
          {
            bundle.Include(i.Attribute("Path").Value);
          }
        }

        bundles.Add(bundle);
      }
    }
  }
}