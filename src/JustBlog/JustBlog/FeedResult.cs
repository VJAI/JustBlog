using System;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace JustBlog
{
  public class FeedResult : ActionResult
  {
    public Encoding ContentEncoding { get; set; }
    public string ContentType { get; set; }

    private readonly SyndicationFeedFormatter _feed;
    public SyndicationFeedFormatter Feed
    {
      get { return _feed; }
    }

    public FeedResult(SyndicationFeedFormatter feed)
    {
      _feed = feed;
    }

    public override void ExecuteResult(ControllerContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");

      var response = context.HttpContext.Response;
      response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/rss+xml";

      if (ContentEncoding != null)
        response.ContentEncoding = ContentEncoding;

      if (_feed != null)
        using (var xmlWriter = new XmlTextWriter(response.Output))
        {
          xmlWriter.Formatting = Formatting.Indented;
          _feed.WriteTo(xmlWriter);
        }
    }
  }
}