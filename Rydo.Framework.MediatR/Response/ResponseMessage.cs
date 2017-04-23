using Rydo.Framework.MediatR.Contratos;

namespace Rydo.Framework.MediatR.Response
{
    public abstract class ResponseMessage : Message
    {
        public dynamic Result { get; protected set; }

        public virtual ResponseStatus Status { get; set; } = new ResponseStatus();
    }
}
    