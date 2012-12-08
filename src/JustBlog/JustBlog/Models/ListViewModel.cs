
using JustBlog.Core;
using JustBlog.Core.Objects;
using System.Collections.Generic;

namespace JustBlog.Models
{
  public class ListViewModel
  {
    public ListViewModel(IBlogRepository blogRepository, int p)
    {
      Posts = blogRepository.Posts(p - 1, 10);
      TotalPosts = blogRepository.TotalPosts();
    }

    public ListViewModel(IBlogRepository blogRepository, string text, string type, int p)
    {
      switch (type)
      {
        case "Category":
          Posts = blogRepository.PostsForCategory(text, p - 1, 10);
          TotalPosts = blogRepository.TotalPostsForCategory(text);
          Category = blogRepository.Category(text);
          break;
        case "Tag":
          Posts = blogRepository.PostsForTag(text, p - 1, 10);
          TotalPosts = blogRepository.TotalPostsForTag(text);
          Tag = blogRepository.Tag(text);
          break;
        default:
          Posts = blogRepository.PostsForSearch(text, p - 1, 10);
          TotalPosts = blogRepository.TotalPostsForSearch(text);
          break;
      }
    }

    public IList<Post> Posts { get; private set; }
    public int TotalPosts { get; private set; }
    public Category Category { get; private set; }
    public Tag Tag { get; private set; }
    public string Search { get; private set; }
  }
}