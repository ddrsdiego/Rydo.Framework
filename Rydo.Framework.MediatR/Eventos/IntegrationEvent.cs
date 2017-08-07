using MediatR;
using System;

namespace Rydo.Framework.MediatR.Eventos
{
    public class IntegrationEvent : IRequest<Unit>
    {
        public IntegrationEvent()
        {
            IntegrationId = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid IntegrationId { get; }
        public DateTime CreationDate { get; }
    }
}
