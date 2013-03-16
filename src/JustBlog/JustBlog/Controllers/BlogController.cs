using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Models;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
  public class BlogController: Controller
  {
    private readonly IBlogRepository _blogRepository;

    public BlogController(IBlogRepository blogRepository)
    {
      _blogRepository = blogRepository;
    }

    public ViewResult Posts(int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, p);
      ViewBag.Title = "Latest Posts";
      return View("List", viewModel);
    }

    public ViewResult Post(int year, int month, string title)
    {
      var post = _blogRepository.Post(year, month, title);

      if (post == null)
        throw new HttpException(404, "Post not found");

      if (post.Published == false && User.Identity.IsAuthenticated == false)
        throw new HttpException(401, "The post is not published");

      return View(post);
    }

    public ViewResult Category(string category, int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

      if (viewModel.Category == null)
        throw new HttpException(404, "Category not found");

      ViewBag.Title = String.Format(@"Latest posts on category ""{0}""", viewModel.Category.Name);
      return View("List", viewModel);
    }

    public ViewResult Tag(string tag, int p = 1)
    {
      var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

      if (viewModel.Tag == null)
        throw new HttpException(404, "Tag not found");

      ViewBag.Title = String.Format(@"Latest posts tagged on ""{0}""", viewModel.Tag.Name);
      return View("List", viewModel);
    }

    public ViewResult Search(string s, int p = 1)
    {
      ViewBag.Title = String.Format(@"Lists of posts found for search text ""{0}""", s);

      var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
      return View("List", viewModel);
    }

    [ChildActionOnly]
    public PartialViewResult Sidebars()
    {
      var widgetViewModel = new WidgetViewModel(_blogRepository);
      return PartialView("_Sidebars", widgetViewModel);
    }

    public ViewResult Contact()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Contact(Contact contact)
    {
      if (ModelState.IsValid)
      {
        using (var client = new SmtpClient())
        {
          var from = new MailAddress("admin@justblog.com", "JustBlog Messenger");
          var to = new MailAddress("admin@justblog.com", "JustBlog Admin");

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

        return RedirectToAction("Posts");
      }

      return View();
    }

    public ViewResult Aboutme()
    {
      return View();
    }
  }
}
