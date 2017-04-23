using MediatR;
using System.Threading.Tasks;

namespace Rydo.Framework.MediatR.IoC
{
    public class DefaultNoOpPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            return next();
        }
    }
}
