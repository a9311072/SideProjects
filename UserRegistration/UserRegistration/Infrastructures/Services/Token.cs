using UserRegistration.Infrastructures.Interfaces;

namespace UserRegistration.Infrastructures.Services
{
    public class Token
    {
        private IToken _token;
        public Token(IToken token)
        {
            _token = token;
        }

        public string GetToken(string userName, int expireMinutes)
        {
            return _token.GetToken(userName, expireMinutes);
        }

        public string GetValidateToken(string token)
        {
            return _token.TryValidateToken(token);
        }
    }
}