using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserRegistration.Infrastructures.Services;

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
                new Notification(new Email("xxxxxxx@gmail.com", "UserName")).DoNotify();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
}
    }
}
