
using JustBlog.Core.Objects;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace JustBlog.Core
{
  public class BlogRepository : IBlogRepository
  {
    private readonly ISession _session;

    public BlogRepository(ISession session)
    {
      _session = session;
    }

    public IList<Post> Posts(int pageNo, int pageSize)
    {
      var query = _session.Query<Post>()
                          .Where(p => p.Published)
                          .OrderByDescending(p => p.PostedOn)
                          .Skip(pageNo * pageSize)
                          .Take(pageSize)
                          .Fetch(p => p.Category);

      query.FetchMany(p => p.Tags).ToFuture();

      return query.ToFuture().ToList();
    }

    public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
    {
      var query = _session.Query<Post>()
                          .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                          .OrderByDescending(p => p.PostedOn)
                          .Skip(pageNo * pageSize)
                          .Take(pageSize)
                          .Fetch(p => p.Category);

      query.FetchMany(p => p.Tags).ToFuture();

      return query.ToFuture().ToList();
    }

    public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
    {
      var query = _session.Query<Post>()
                         .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                         .OrderByDescending(p => p.PostedOn)
                         .Skip(pageNo * pageSize)
                         .Take(pageSize)
                         .Fetch(p => p.Category);

      query.FetchMany(p => p.Tags).ToFuture();

      return query.ToFuture().ToList();
    }

    public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
    {
      var query = _session.Query<Post>()
                     .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);

      query.FetchMany(p => p.Tags).ToFuture();

      return query.ToFuture().ToList();
    }

    public int TotalPosts(bool checkIsPublished = true)
    {
      return _session.Query<Post>().Where(p => checkIsPublished || p.Published == true).Count();
    }

    public int TotalPostsForCategory(string categorySlug)
    {
      return _session.Query<Post>()
                    .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                    .Count();
    }

    public int TotalPostsForTag(string tagSlug)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                     .Count();
    }

    public int TotalPostsForSearch(string search)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                     .Count();
    }

    public IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
    {
      IQueryable<Post> query;

      switch (sortColumn)
      {
        case "Title":
          if(sortByAscending)
            query = _session.Query<Post>()
                     .OrderBy(p => p.Title)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          else
            query = _session.Query<Post>()
                     .OrderByDescending(p => p.Title)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          break;
        case "Published":
          if (sortByAscending)
            query = _session.Query<Post>()
                     .OrderBy(p => p.Published)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          else
            query = _session.Query<Post>()
                     .OrderByDescending(p => p.Published)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          break;
        case "PostedOn":
          if (sortByAscending)
            query = _session.Query<Post>()
                     .OrderBy(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          else
            query = _session.Query<Post>()
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          break;
        case "Modified":
          if (sortByAscending)
            query = _session.Query<Post>()
                     .OrderBy(p => p.Modified)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          else
            query = _session.Query<Post>()
                     .OrderByDescending(p => p.Modified)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          break;
        case "Category":
          if (sortByAscending)
            query = _session.Query<Post>()
                     .OrderBy(p => p.Category.Name)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          else
            query = _session.Query<Post>()
                     .OrderByDescending(p => p.Category.Name)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category);
          break;
        default:
          query = _session.Query<Post>()
                   .OrderByDescending(p => p.PostedOn)
                   .Skip(pageNo * pageSize)
                   .Take(pageSize)
                   .Fetch(p => p.Category);
          break;
      }

      query.FetchMany(p => p.Tags).ToFuture();

      return query.ToFuture().ToList();
    }

    public Post Post(int year, int month, string titleSlug)
    {
      return _session.Query<Post>()
                     .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .FirstOrDefault();
    }

    public Post Post(int id)
    {
      return _session.Get<Post>(id);
    }

    public int AddPost(Post post)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.Save(post);
        tran.Commit();
        return post.Id;
      }
    }

    public void EditPost(Post post)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.SaveOrUpdate(post);
        tran.Commit();
      }
    }

    public void DeletePost(int id)
    {
      var post = _session.Get<Post>(id);
      if (post != null) _session.Delete(post);
    }

    public IList<Tag> Tags()
    {
      return _session.Query<Tag>().OrderBy(p => p.Name).ToList();
    }

    public int TotalTags()
    {
      return _session.Query<Tag>().Count();
    }

    public Tag Tag(string slug)
    {
      return _session.Query<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
    }

    public Tag Tag(int id)
    {
      return _session.Query<Tag>().FirstOrDefault(t => t.Id == id);
    }

    public int AddTag(Tag tag)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.Save(tag);
        tran.Commit();
        return tag.Id;
      }
    }

    public void EditTag(Tag tag)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.SaveOrUpdate(tag);
        tran.Commit();
      }
    }

    public void DeleteTag(int id)
    {
      using (var tran = _session.BeginTransaction())
      {
        var tag = _session.Get<Tag>(id);
        _session.Delete(tag);
        tran.Commit();
      }
    }

    public IList<Category> Categories()
    {
      return _session.Query<Category>().OrderBy(p => p.Name).ToList();
    }

    public int TotalCategories()
    {
      return _session.Query<Category>().Count();
    }

    public Category Category(string slug)
    {
      return _session.Query<Category>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
    }

    public Category Category(int id)
    {
      return _session.Query<Category>().FirstOrDefault(t => t.Id == id);
    }

    public int AddCategory(Category category)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.Save(category);
        tran.Commit();
        return category.Id;
      }
    }

    public void EditCategory(Category category)
    {
      using (var tran = _session.BeginTransaction())
      {
        _session.SaveOrUpdate(category);
        tran.Commit();
      }
    }

    public void DeleteCategory(int id)
    {
      using (var tran = _session.BeginTransaction())
      {
        var category = _session.Get<Category>(id);
        _session.Delete(category);
        tran.Commit();
      }
    }
  }
}
