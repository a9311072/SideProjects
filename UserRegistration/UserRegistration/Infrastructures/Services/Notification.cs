using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserRegistration.Infrastructures.Interface;

namespace UserRegistration.Infrastructures.Services
{
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
}