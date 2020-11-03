using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserRegistration.Services;

namespace UserRegistration.Tests.Services
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void JwtTokenTest()
        {
            var token = new Token(new JwtToken()).GetToken("Allen", 120);
            Assert.IsNotNull(token);
            var result = new Token(new JwtToken()).GetValidateToken(token);
            Assert.AreNotEqual(result, "Token has expired");
            Assert.AreNotEqual(result, "Token has invalid signature");
        }
    }
}
