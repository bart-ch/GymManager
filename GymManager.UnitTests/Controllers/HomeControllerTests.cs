using GymManager.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace GymManager.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new HomeController(); 
        }

        [Test]
        public void Index_WhenUserIsLoggedIn_ReturnViewForLoggedInUser()
        {
            var user = CreateLoggedInUser();
            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.Setup(o => o.HttpContext.User).Returns(user);
            controller.ControllerContext = mockControllerContext.Object;
            var loggedInUserViewName = "LoggedInIndex";

            var result = controller.Index();

            Assert.That(result.ViewName, Is.EqualTo(loggedInUserViewName)); ;
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void Index_WhenUserIsNotLoggedIn_ReturnView()
        {
            CreateNotLoggedInUser();

            var result = controller.Index();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty)); ;
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        [Test]
        public void Contact_WhenCalled_ReturnView()
        {
            var result = controller.Contact();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
            Assert.That(result, Is.TypeOf(typeof(ViewResult)));
        }

        private ClaimsPrincipal CreateLoggedInUser()
        {
            var identity = new GenericIdentity("TestUser");
            identity.AddClaims(new List<Claim> {
            new Claim(ClaimTypes.Sid, "UserId")});

            return new ClaimsPrincipal(identity);
        }

        private void CreateNotLoggedInUser()
        {
            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> principal = mocks.Create<IPrincipal>();
            principal.SetupGet(p => p.Identity.Name).Returns("UserName");

            var mockContext = new Mock<ControllerContext>();
            mockContext.SetupGet(p => p.HttpContext.User).Returns(principal.Object);
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(false);
            controller.ControllerContext = mockContext.Object;
        }
    }
}
