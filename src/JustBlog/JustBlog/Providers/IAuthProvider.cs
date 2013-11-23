
namespace JustBlog.Providers
{
  /// <summary>
  /// Defines methods for login, logout.
  /// </summary>
  public interface IAuthProvider
  {
    /// <summary>
    /// Return True if the user is already logged-in.
    /// </summary>
    bool IsLoggedIn { get; }

    /// <summary>
    /// Authenticate an user and set cookie if user is valid.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    bool Login(string username, string password);

    /// <summary>
    /// Logout the user.
    /// </summary>
    void Logout();
  }
}
