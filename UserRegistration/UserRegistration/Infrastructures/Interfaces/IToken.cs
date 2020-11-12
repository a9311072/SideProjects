namespace UserRegistration.Infrastructures.Interfaces
{
    public interface IToken
    {
        string GetToken(string userName, int expireMinutes);
        string TryValidateToken(string token);
    }
}
