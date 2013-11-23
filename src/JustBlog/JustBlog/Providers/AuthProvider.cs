
using System.Web;
using System.Web.Security;

namespace JustBlog.Providers
{
  public class AuthProvider: IAuthProvider
  {
    /// <summary>
    /// Return True if the user is already logged-in.
    /// </summary>
    public bool IsLoggedIn
    {
      get
      {
        return HttpContext.Current.User.Identity.IsAuthenticated;
      }
    }

    /// <summary>
    /// Authenticate an user and set cookie if user is valid.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool Login(string username, string password)
    {
      var result = FormsAuthentication.Authenticate(username, password); // TODO: User Membership APIs

      if (result)
        FormsAuthentication.SetAuthCookie(username, false);

      return result;
    }

    /// <summary>
    /// Logout the user.
    /// </summary>
    public void Logout()
    {
      FormsAuthentication.SignOut();
    }
  }
}