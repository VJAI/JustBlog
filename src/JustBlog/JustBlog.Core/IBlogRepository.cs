
using JustBlog.Core.Objects;
using System.Collections.Generic;

namespace JustBlog.Core
{
  /// <summary>
  /// Defines all the database methods required for JustBlog.
  /// </summary>
  public interface IBlogRepository
  {
    /// <summary>
    /// Return collection of posts based on pagination parameters.
    /// </summary>
    /// <param name="pageNo">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    IList<Post> Posts(int pageNo, int pageSize);

    /// <summary>
    /// Return collection of posts belongs to a particular tag.
    /// </summary>
    /// <param name="tagSlug">Tag's url slug</param>
    /// <param name="pageNo">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);

    /// <summary>
    /// Return collection of posts belongs to a particular category.
    /// </summary>
    /// <param name="categorySlug">Category's url slug</param>
    /// <param name="pageNo">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);

    /// <summary>
    /// Return collection of posts that matches the search text.
    /// </summary>
    /// <param name="search">search text</param>
    /// <param name="pageNo">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <returns></returns>
    IList<Post> PostsForSearch(string search, int pageNo, int pageSize);

    /// <summary>
    /// Return total no. of all or published posts.
    /// </summary>
    /// <param name="checkIsPublished">True to count only published posts</param>
    /// <returns></returns>
    int TotalPosts(bool checkIsPublished = true);

    /// <summary>
    /// Return total no. of posts belongs to a particular category.
    /// </summary>
    /// <param name="categorySlug">Category's url slug</param>
    /// <returns></returns>
    int TotalPostsForCategory(string categorySlug);

    /// <summary>
    /// Return total no. of posts belongs to a particular tag.
    /// </summary>
    /// <param name="tagSlug">Tag's url slug</param>
    /// <returns></returns>
    int TotalPostsForTag(string tagSlug);

    /// <summary>
    /// Return total no. of posts that matches the search text.
    /// </summary>
    /// <param name="search">search text</param>
    /// <returns></returns>
    int TotalPostsForSearch(string search);

    /// <summary>
    /// Return posts based on pagination and sorting parameters.
    /// </summary>
    /// <param name="pageNo">Page index</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="sortColumn">Sort column name</param>
    /// <param name="sortByAscending">True to sort by ascending</param>
    /// <returns></returns>
    IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

    /// <summary>
    /// Return post based on the published year, month and title slug.
    /// </summary>
    /// <param name="year">Published year</param>
    /// <param name="month">Published month</param>
    /// <param name="titleSlug">Post's url slug</param>
    /// <returns></returns>
    Post Post(int year, int month, string titleSlug);

    /// <summary>
    /// Return post based on unique id.
    /// </summary>
    /// <param name="id">Post unique id</param>
    /// <returns></returns>
    Post Post(int id);

    /// <summary>
    /// Adds a new post and returns the id.
    /// </summary>
    /// <param name="post"></param>
    /// <returns>Newly added post id</returns>
    int AddPost(Post post);

    /// <summary>
    /// Update an existing post.
    /// </summary>
    /// <param name="post"></param>
    void EditPost(Post post);

    /// <summary>
    /// Delete the post permanently from database.
    /// </summary>
    /// <param name="id"></param>
    void DeletePost(int id);

    /// <summary>
    /// Return all the categories.
    /// </summary>
    /// <returns></returns>
    IList<Category> Categories();

    /// <summary>
    /// Return total no. of categories.
    /// </summary>
    /// <returns></returns>
    int TotalCategories();

    /// <summary>
    /// Return category based on url slug.
    /// </summary>
    /// <param name="categorySlug">Category's url slug</param>
    /// <returns></returns>
    Category Category(string categorySlug);

    /// <summary>
    /// Return category based on id.
    /// </summary>
    /// <param name="id">Category id</param>
    /// <returns></returns>
    Category Category(int id);

    /// <summary>
    /// Add a new category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    int AddCategory(Category category);

    /// <summary>
    /// Update an existing category.
    /// </summary>
    /// <param name="category"></param>
    void EditCategory(Category category);

    /// <summary>
    /// Delete a category permanently.
    /// </summary>
    /// <param name="id"></param>
    void DeleteCategory(int id);

    /// <summary>
    /// Return all the tags.
    /// </summary>
    /// <returns></returns>
    IList<Tag> Tags();

    /// <summary>
    /// Return total no. of tags.
    /// </summary>
    /// <returns></returns>
    int TotalTags();

    /// <summary>
    /// Return tag based on url slug.
    /// </summary>
    /// <param name="tagSlug"></param>
    /// <returns></returns>
    Tag Tag(string tagSlug);

    /// <summary>
    /// Return tag based on unique id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Tag Tag(int id);

    /// <summary>
    /// Add a new tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    int AddTag(Tag tag);

    /// <summary>
    /// Edit an existing tag.
    /// </summary>
    /// <param name="tag"></param>
    void EditTag(Tag tag);

    /// <summary>
    /// Delete an existing tag permanently.
    /// </summary>
    /// <param name="id"></param>
    void DeleteTag(int id);
  }
}
