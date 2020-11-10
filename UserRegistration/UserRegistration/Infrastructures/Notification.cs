using System;
using System.Net.Mail;

namespace UserRegistration.Infrastructures
{
    public interface IMessenger
    {
        void SendMessage();
    }

    public class Notification
    {
        private IMessenger _messenger;
        public Notification(IMessenger messenger)
        {
            _messenger = messenger;
        }
        public void DoNotify()
        {
            _messenger.SendMessage();
        }
    }

    public class Email : IMessenger
    {
        private readonly string _address;
        private readonly string _name;
        public Email(string address, string name)
        {
            _address = address;
            _name = name;
        }
        public void SendMessage()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(_address);

                msg.From = new MailAddress("a9311072@gmail.com", "陳正宗", System.Text.Encoding.UTF8);             
                msg.Subject = _name + " 您好！ 歡迎加入 AmazingTalker";
                msg.SubjectEncoding = System.Text.Encoding.UTF8;        
                msg.Body = "您好！ 歡迎加入 AmazingTalker"; 
                msg.BodyEncoding = System.Text.Encoding.UTF8;  
                msg.IsBodyHtml = true;                                      

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("a9311072@gmail.com", "xxxxxxxxxxxxxxxxx");
                client.EnableSsl = true;            
                client.Send(msg);                   
                client.Dispose();
                msg.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SMS : IMessenger
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