using CQRSlite.Events;
using System;

namespace Rydo.Framework.MediatR.Eventos
{
    public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public int Version { get; set; }
    }
}
