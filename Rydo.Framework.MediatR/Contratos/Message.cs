using System;

namespace Rydo.Framework.MediatR.Contratos
{
    public abstract class Message : IMessage
    {
        public Message()
        {
            MessageId = Guid.NewGuid().ToString("N");
            MessageDate = DateTime.Now;
        }

        public string MessageId { get; private set; }
        public DateTime MessageDate { get; private set; }
    }
}
