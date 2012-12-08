
using System.Collections.Generic;

namespace JustBlog.Core.Objects
{
  public class Tag
  {
    public virtual int Id
    { get; set; }

    public virtual string Name
    { get; set; }

    public virtual string UrlSlug
    { get; set; }

    public virtual string Description
    { get; set; }

    public virtual IList<Post> Posts
    { get; set; }
  }
}
