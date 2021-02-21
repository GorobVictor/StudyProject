using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MessageSender
    {
        private IMessageSender _message { get; }

        public MessageSender(IMessageSender message)
        {
            _message = message;
        }

        public string Send() => _message.Send();
    }
}
