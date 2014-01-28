using System;
using System.Web;
using JustBlog.Controllers;
using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Providers;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Configuration;

namespace JustBlog
{
  public class MvcApplication : NinjectHttpApplication
  {
    protected override IKernel CreateKernel()
    {
      var kernel = new StandardKernel();

      kernel.Load(new RepositoryModule());
      kernel.Bind<IBlogRepository>().To<BlogRepository>();
      kernel.Bind<IAuthProvider>().To<AuthProvider>();

      return kernel;
    }

    protected override void OnApplicationStarted()
    {
      var theme = ConfigurationManager.AppSettings["Theme"];
      theme = String.IsNullOrEmpty(theme) ? "default" : theme;

      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles, theme);

      ViewEngines.Engines.Add(new ThemedRazorViewEngine(theme));

      ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));

      HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

      base.OnApplicationStarted();
    }

    protected void Application_Error(object sender, EventArgs e)
    {
      var httpContext = ((MvcApplication)sender).Context;
      var ex = Server.GetLastError();
      var status = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;

      // Is Ajax request? return json
      if (httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
      {
        httpContext.ClearError();
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = status;
        httpContext.Response.TrySkipIisCustomErrors = true;
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.Write("{ success: false, message: \"Error occured in server.\" }");
        httpContext.Response.End();
      }
      else
      {
        var currentController = " ";
        var currentAction = " ";
        var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

        if (currentRouteData != null)
        {
          if (currentRouteData.Values["controller"] != null &&
              !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
          {
            currentController = currentRouteData.Values["controller"].ToString();
          }

          if (currentRouteData.Values["action"] != null &&
              !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
          {
            currentAction = currentRouteData.Values["action"].ToString();
          }
        }

        var controller = new ErrorController();
        var routeData = new RouteData();

        httpContext.ClearError();
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = status;
        httpContext.Response.TrySkipIisCustomErrors = true;

        routeData.Values["controller"] = "Error";
        routeData.Values["action"] = "Index";

        controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
        ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
      }
    }
  }
}
