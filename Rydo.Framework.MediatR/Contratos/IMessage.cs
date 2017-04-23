using System;

namespace Rydo.Framework.MediatR.Contratos
{
    public interface IMessage
    {
        string MessageId { get; }
        DateTime MessageDate { get; }
    }
}
