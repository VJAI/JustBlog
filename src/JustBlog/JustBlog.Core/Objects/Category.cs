
#region Usings
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#endregion

namespace JustBlog.Core.Objects
{
  /// <summary>
  /// Represents a category that contains group of blog posts.
  /// </summary>
  public class Category
  {
    public virtual int Id
    { get; set; }

    [Required(ErrorMessage = "Name: Field is required")]
    [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
    public virtual string Name
    { get; set; }

    [Required(ErrorMessage = "UrlSlug: Field is required")]
    [StringLength(500, ErrorMessage = "UrlSlug: Length should not exceed 500 characters")]
    public virtual string UrlSlug
    { get; set; }

    public virtual string Description
    { get; set; }

    [JsonIgnore]
    public virtual IList<Post> Posts
    { get; set; }
  }
}
