
using JustBlog.Core.Objects;
using System.Collections.Generic;

namespace JustBlog.Core
{
  public interface IBlogRepository
  {
    IList<Post> Posts(int pageNo, int pageSize);
    IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
    IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
    IList<Post> PostsForSearch(string search, int pageNo, int pageSize);

    int TotalPosts();
    int TotalPostsForCategory(string categorySlug);
    int TotalPostsForTag(string tagSlug);
    int TotalPostsForSearch(string search);

    Post Post(int year, int month, string titleSlug);

    IList<Category> Categories();
    Category Category(string categorySlug);

    IList<Tag> Tags();
    Tag Tag(string tagSlug);
  }
}
