using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Controllers;
using UserRegistration.Models;
using UserRegistration.Repositories;
using UserRegistration.Services;

namespace UserRegistration.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        private readonly string badReguest = "System.Web.Http.Results.BadRequestErrorMessageResult";
        [TestMethod]
        public void PostEmailAndPhoneIsNullTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User {
                Name = "Allen",
                Email = null,
                Phone = null,
                CountryCode = null,
                Password = "12345678",
                ConfirmPassword = "12345678"
            });            
            Assert.AreEqual(obj.Exception, null );
            Assert.AreEqual(obj.Result.ToString(), badReguest);
        }

        public void PostEmailIsNullTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User
            {
                Name = "Allen",
                Email = null,
                Phone = "0955370810",
                CountryCode = "886",
                Password = "12345678",
                ConfirmPassword = "12345678"
            });
            Assert.AreEqual(obj.Exception, null);
            Assert.AreEqual(obj.Result.ToString(), badReguest);
        }

        public void PostPhoneIsNullTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User
            {
                Name = "Allen",
                Email = "test@gmail.com",
                Phone = null,
                CountryCode = null,
                Password = "12345678",
                ConfirmPassword = "12345678"
            });
            Assert.AreEqual(obj.Exception, null);
            Assert.AreEqual(obj.Result.ToString(), badReguest);

        }

        public void PostEmailIsUsedTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User
            {
                Name = "Allen",
                Email = "admin@gmail.com",
                Phone = null,
                CountryCode = null,
                Password = "12345678",
                ConfirmPassword = "12345678"
            });
            Assert.AreEqual(obj.Exception, null);
            Assert.AreEqual(obj.Result.ToString(), badReguest);
        }

        public void PostPhoneIsUsedTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User
            {
                Name = "Allen",
                Email = null,
                Phone = "12345678",
                CountryCode = "12345678",
                Password = "12345678",
                ConfirmPassword = "12345678"
            });
            Assert.AreEqual(obj.Exception, null);
            Assert.AreEqual(obj.Result.ToString(), badReguest);
        }

        public void PostPasswordInconsistencyTest()
        {
            UserController controller = new UserController();
            var obj = controller.Post(new User
            {
                Name = "Allen",
                Email = null,
                Phone = "1234567899",
                CountryCode = "899",
                Password = "12345678",
                ConfirmPassword = "81234567"
            });
            Assert.AreEqual(obj.Exception, null);
            Assert.AreEqual(obj.Result.ToString(), badReguest);
        }
    }
}
