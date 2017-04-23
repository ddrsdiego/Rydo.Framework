using MediatR;
using Rydo.Framework.MediatR.Contratos;
using Rydo.Framework.MediatR.Response;

namespace Rydo.Framework.MediatR.Command
{
    public abstract class CommandMessageAsync<TResponseMessageAsync> : Message, IAsyncRequest<TResponseMessageAsync>
        where TResponseMessageAsync : ResponseMessageAsync
    {
        public virtual TResponseMessageAsync Response { get; }
    }
}
