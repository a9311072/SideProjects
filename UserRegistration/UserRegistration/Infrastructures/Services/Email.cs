using System;
using System.Net.Mail;
using UserRegistration.Infrastructures.Interface;

namespace UserRegistration.Infrastructures.Services
{
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

                msg.From = new MailAddress("xxxxxxxxx@gmail.com", "PleaseInputTheUserName", System.Text.Encoding.UTF8);
                msg.Subject = _name + " 您好！ 歡迎加入 AmazingTalker";
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.Body = "您好！ 歡迎加入 AmazingTalker";
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("xxxxxxx@gmail.com", "PleaseInputThePassword");
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
}