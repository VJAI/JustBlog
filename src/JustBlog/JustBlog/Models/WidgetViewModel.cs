
using JustBlog.Core;
using JustBlog.Core.Objects;
using System.Collections.Generic;

namespace JustBlog.Models
{
  public class WidgetViewModel
  {
    public WidgetViewModel(IBlogRepository blogRepository)
    {
      Categories = blogRepository.Categories();
      Tags = blogRepository.Tags();
      LatestPosts = blogRepository.Posts(0, 10);
    }

    public IList<Category> Categories { get; private set; }
    public IList<Tag> Tags { get; private set; }
    public IList<Post> LatestPosts { get; private set; }
  }
}