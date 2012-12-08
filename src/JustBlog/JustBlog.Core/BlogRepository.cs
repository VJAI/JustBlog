
using JustBlog.Core.Objects;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System.Collections.Generic;
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
      return _session.Query<Post>()
                     .Where(p => p.Published)
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .ToList();
    }

    public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .ToList();
    }

    public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .ToList();
    }

    public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                     .OrderByDescending(p => p.PostedOn)
                     .Skip(pageNo * pageSize)
                     .Take(pageSize)
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .ToList();
    }

    public int TotalPosts()
    {
      return _session.Query<Post>().Where(p => p.Published).Count();
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

    public Post Post(int year, int month, string titleSlug)
    {
      return _session.Query<Post>()
                     .Where(p => p.Published && p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                     .Fetch(p => p.Category)
                     .FetchMany(p => p.Tags)
                     .FirstOrDefault();
    }

    public IList<Tag> Tags()
    {
      return _session.Query<Tag>().ToList();
    }

    public Tag Tag(string slug)
    {
      return _session.Query<Tag>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
    }
  
    public IList<Category> Categories()
    {
      return _session.Query<Category>().ToList();
    }

    public Category Category(string slug)
    {
      return _session.Query<Category>().FirstOrDefault(t => t.UrlSlug.Equals(slug));
    } 
  }
}
