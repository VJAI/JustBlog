#region Usings
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Models;
#endregion

namespace JustBlog.Controllers
{
  /// <summary>
  /// Home controller that contain actions to return list/view pages and others.
  /// </summary>
  public class BlogController: Controller
  {
    private readonly IBlogRepository _blogRepository;

    public BlogController(IBlogRepository blogRepository)
    {
      _blogRepository = blogRepository;
    }

    /// <summary>
    /// Return the list page with latest posts.
    /// </summary>
    /// <param name="p">pagination number</param>
    /// <returns></returns>
    public ViewResult Posts(int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, p);
      ViewBag.Title = "Latest Posts";
      return View("List", viewModel);
    }

    /// <summary>
    /// Return a particular post based on the puslished year, month and url slug.
    /// </summary>
    /// <param name="year">Published year</param>
    /// <param name="month">Published month</param>
    /// <param name="title">Url slug</param>
    /// <returns></returns>
    public ViewResult Post(int year, int month, string title)
    {
      var post = _blogRepository.Post(year, month, title);

      if (post == null)
        throw new HttpException(404, "Post not found");

      if (post.Published == false && User.Identity.IsAuthenticated == false)
        throw new HttpException(401, "The post is not published");

      return View(post);
    }

    /// <summary>
    /// Return the posts belongs to a category.
    /// </summary>
    /// <param name="category">Url slug</param>
    /// <param name="p">Pagination number</param>
    /// <returns></returns>
    public ViewResult Category(string category, int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

      if (viewModel.Category == null)
        throw new HttpException(404, "Category not found");

      ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.Name);
      return View("List", viewModel);
    }

    /// <summary>
    /// Return the posts belongs to a tag.
    /// </summary>
    /// <param name="tag">Url slug</param>
    /// <param name="p">Pagination number</param>
    /// <returns></returns>
    public ViewResult Tag(string tag, int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

      if (viewModel.Tag == null)
        throw new HttpException(404, "Tag not found");

      ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
      return View("List", viewModel);
    }

    /// <summary>
    /// Return the posts that matches the search text.
    /// </summary>
    /// <param name="s">search text</param>
    /// <param name="p">Pagination number</param>
    /// <returns></returns>
    public ViewResult Search(string s, int p = 1)
    {
      ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);

      var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
      return View("List", viewModel);
    }

    /// <summary>
    /// Child action that returns the sidebar partial view.
    /// </summary>
    /// <returns></returns>
    [ChildActionOnly]
    public PartialViewResult Sidebars()
    {
      var widgetViewModel = new WidgetViewModel(_blogRepository);
      return PartialView("_Sidebars", widgetViewModel);
    }

    /// <summary>
    /// Return the contact form.
    /// </summary>
    /// <returns></returns>
    public ViewResult Contact()
    {
      return View();
    }

    /// <summary>
    /// Send an email to the blog admin from the POSTed contact form.
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>
    [HttpPost]
    public ViewResult Contact(Contact contact)
    {
      if (ModelState.IsValid)
      {
        using (var client = new SmtpClient())
        {
          var adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
          var from = new MailAddress(adminEmail, "JustBlog Messenger");
          var to = new MailAddress(adminEmail, "JustBlog Admin");

          using (var message = new MailMessage(from, to))
          {
            message.Body = contact.Body;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;

            message.Subject = contact.Subject;
            message.SubjectEncoding = Encoding.UTF8;

            message.ReplyTo = new MailAddress(contact.Email);

            client.Send(message);
          }
        }

        return View("Thanks");
      }

      return View();
    }

    /// <summary>
    /// Return the About me page.
    /// </summary>
    /// <returns></returns>
    public ViewResult Aboutme()
    {
      return View();
    }

    /// <summary>
    /// Generate and return RSS feed.
    /// </summary>
    /// <returns></returns>
    public ActionResult Feed()
    {
      var blogTitle = ConfigurationManager.AppSettings["BlogTitle"];
      var blogDescription = ConfigurationManager.AppSettings["BlogDescription"];
      var blogUrl = ConfigurationManager.AppSettings["BlogUrl"];

      var posts = _blogRepository.Posts(0, 25).Select
      (
          p => new SyndicationItem
              (
                  p.Title,
                  p.Description,
                  new Uri(string.Concat(blogUrl, p.Href(Url)))
              )
      );

      var feed = new SyndicationFeed(blogTitle, blogDescription, new Uri(blogUrl), posts)
      {
        Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", blogTitle)),
        Language = "en-US"
      };

      return new FeedResult(new Rss20FeedFormatter(feed));
    }
  }
}
