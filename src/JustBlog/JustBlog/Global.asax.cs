using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Providers;
using Ninject;
using Ninject.Web.Common;
using System.Web.Mvc;
using System.Web.Routing;

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
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));

      base.OnApplicationStarted();
    }
  }
}