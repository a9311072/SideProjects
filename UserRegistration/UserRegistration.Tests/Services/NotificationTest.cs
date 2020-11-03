using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Services;
using UserRegistration.Models;

namespace UserRegistration.Tests.Services
{
    [TestClass]
    public class NotificationTest
    {
        [TestMethod]
        public void Notification()
        {
            try
            {
                //Please setup account& password before testing
                new Notification(new Email("lurker_ama@hotmail.com", "Allen")).DoNotify();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
}
    }
}
