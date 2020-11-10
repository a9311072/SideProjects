using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserRegistration.Infrastructures;

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
