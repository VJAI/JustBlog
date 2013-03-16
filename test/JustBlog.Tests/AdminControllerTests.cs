
using JustBlog.Controllers;
using JustBlog.Models;
using JustBlog.Providers;
using NUnit.Framework;
using Rhino.Mocks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JustBlog.Tests
{
  [TestFixture]
  public class AdminControllerTests
  {
    private AdminController _adminController;
    private IAuthProvider _authProvider;

    [SetUp]
    public void SetUp()
    {
      _authProvider = MockRepository.GenerateMock<IAuthProvider>();
      _adminController = new AdminController(_authProvider);

      var httpContextMock = MockRepository.GenerateMock<HttpContextBase>();
      _adminController.Url = new UrlHelper(new RequestContext(httpContextMock, new RouteData()));
    }

    /// <summary>
    /// This test is to verify if the user is already logged-in then the action should
    /// redirect the user to the url passed to the action.
    /// </summary>
    [Test]
    public void Login_IsLoggedIn_True_Test()
    {
      // arrange
      _authProvider.Stub(s => s.IsLoggedIn).Return(true);

      // act
      var actual = _adminController.Login("/admin/manage");

      // assert
      Assert.IsInstanceOf<RedirectResult>(actual);
      Assert.AreEqual("/admin/manage", ((RedirectResult)actual).Url);
    }

    /// <summary>
    /// This test is to verify if the user is not logged then the action should return the login view.
    /// </summary>
    [Test]
    public void Login_IsLoggedIn_False_Test()
    {
      // arrange
      _authProvider.Stub(s => s.IsLoggedIn).Return(false);

      // act
      var actual = _adminController.Login("/");

      // assert
      Assert.IsInstanceOf<ViewResult>(actual);
      Assert.AreEqual("/", ((ViewResult)actual).ViewBag.ReturnUrl);
    }

    /// <summary>
    /// This test is to verify if there are validation errors then the action should
    /// return the same login view with errors.
    /// </summary>
    [Test]
    public void Login_Post_Model_Invalid_Test()
    {
      // arrange
      var model = new LoginModel();
      _adminController.ModelState.AddModelError("UserName", "UserName is required");

      // act
      var actual = _adminController.Login(model, "/");

      // assert
      Assert.IsInstanceOf<ViewResult>(actual);
    }

    /// <summary>
    /// This test is to verify if the user in invalid then the action should return the 
    /// same view with corresponding error.
    /// </summary>
    [Test]
    public void Login_Post_User_Invalid_Test()
    {
      // arrange
      var model = new LoginModel { UserName = "invaliduser", Password = "password" };
      _authProvider.Stub(s => s.Login(model.UserName, model.Password)).Return(false);

      // act
      var actual = _adminController.Login(model, "/");

      // assert
      Assert.IsInstanceOf<ViewResult>(actual);
      var modelStateErrors = _adminController.ModelState[""].Errors;
      Assert.IsTrue(modelStateErrors.Count > 0);
      Assert.AreEqual("The user name or password provided is incorrect.", modelStateErrors[0].ErrorMessage);
    }


    /// <summary>
    /// This test is to verify if the user is valid the action should redirect the user to 
    /// the corresponding url he was trying to access.
    /// </summary>
    [Test]
    public void Login_Post_User_Valid_Test()
    {
      // arrange
      var model = new LoginModel { UserName = "validuser", Password = "password" };
      _authProvider.Stub(s => s.Login(model.UserName, model.Password)).Return(true);

      // act
      var actual = _adminController.Login(model, "/");

      // assert
      Assert.IsInstanceOf<RedirectResult>(actual);
      Assert.AreEqual("/", ((RedirectResult)actual).Url);
    }
  }
}
