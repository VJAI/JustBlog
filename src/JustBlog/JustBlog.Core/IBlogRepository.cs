
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

    int TotalPosts(bool checkIsPublished = true);
    int TotalPostsForCategory(string categorySlug);
    int TotalPostsForTag(string tagSlug);
    int TotalPostsForSearch(string search);

    IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
    Post Post(int year, int month, string titleSlug);
    Post Post(int id);
    int AddPost(Post post);
    void EditPost(Post post);
    void DeletePost(int id);

    IList<Category> Categories();
    int TotalCategories();
    Category Category(string categorySlug);
    Category Category(int id);
    int AddCategory(Category category);
    void EditCategory(Category category);
    void DeleteCategory(int id);

    IList<Tag> Tags();
    int TotalTags();
    Tag Tag(string tagSlug);
    Tag Tag(int id);
    int AddTag(Tag tag);
    void EditTag(Tag tag);
    void DeleteTag(int id);
  }
}
