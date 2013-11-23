#region Usings
using JustBlog.Core;
using JustBlog.Core.Objects;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
#endregion

namespace JustBlog
{
  /// <summary>
  /// Bind POST model to actions.
  /// </summary>
  public class PostModelBinder : DefaultModelBinder
  {
    private readonly IKernel _kernel;

    public PostModelBinder(IKernel kernel)
    {
      _kernel = kernel;
    }

    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var post = (Post)base.BindModel(controllerContext, bindingContext);

      var blogRepository = _kernel.Get<IBlogRepository>();

      if (post.Category != null)
        post.Category = blogRepository.Category(post.Category.Id);

      var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');

      if (tags.Length > 0)
      {
        post.Tags = new List<Tag>();

        foreach (var tag in tags)
        {
          post.Tags.Add(blogRepository.Tag(int.Parse(tag.Trim())));
        }
      }

      if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
        post.Modified = DateTime.UtcNow; // dates are stored in UTC timezone.
      else
        post.PostedOn = DateTime.UtcNow;

      return post;
    }
  }
}