using MediatR;
using Rydo.Framework.MediatR.Eventos;
using System.Threading.Tasks;

namespace Rydo.Framework.MediatR.Handlres
{
    public abstract class IntegrationEventHandler<TIntegrationEvent, TUnit> : Handler, IAsyncRequestHandler<TIntegrationEvent, Unit>
        where TIntegrationEvent : IntegrationEvent
    {
        public IntegrationEventHandler(IMediator mediator) 
            : base(mediator)
        {
        }

        public abstract Task<Unit> Handle(TIntegrationEvent message);
    }

}
