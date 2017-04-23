using MediatR;

namespace Rydo.Framework.MediatR.Handlres
{
    public abstract class Handler
    {
        protected IMediator Mediator;

        public Handler(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
