
namespace JustBlog.Models
{
  /// <summary>
  /// Used to capture jQGrid input parameters.
  /// </summary>
  public class JqInViewModel
  {
    /// <summary>
    /// No. of records to return.
    /// </summary>
    public int rows { get; set; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int page { get; set; }

    /// <summary>
    /// Sort column name.
    /// </summary>
    public string sidx { get; set; }

    /// <summary>
    /// Sort direction (ex. asc).
    /// </summary>
    public string sord { get; set; }
  }
}