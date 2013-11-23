
using System.ComponentModel.DataAnnotations;

namespace JustBlog.Core.Objects
{
  /// <summary>
  /// Encapsulates the information submitted by the contact form.
  /// </summary>
  public class Contact
  {
    /// <summary>
    /// The user name.
    /// </summary>
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Url]
    public string Website { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public string Body { get; set; }
  }
}
