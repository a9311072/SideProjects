using UserRegistration.Infrastructures.Interface;

namespace UserRegistration.Infrastructures.Services
{
    public class SMS: IMessenger
    {
        private readonly string _phone;
        private readonly string _code;
        private readonly string _name;
        public SMS(string phone, string code, string name)
        {
            _phone = phone;
            _code = code;
            _name = name;
        }

        public void SendMessage()
        {
            // code to send SMS
        }

    }
}