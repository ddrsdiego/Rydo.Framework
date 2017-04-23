using MediatR;
using Rydo.Framework.MediatR.Handlres;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rydo.Framework.MediatR.IoC
{
    public class RegistrarRydoFrameworkMediatR
    {
        public static void Registrar(Container container)
        {
            var assemblies = GetAssemblies().ToArray();

            container.RegisterSingleton<IMediator, Mediator>();

            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.Register(typeof(HandlerRequest<,>), assemblies);
            container.RegisterCollection(typeof(IPipelineBehavior<,>), new[] { typeof(DefaultNoOpPipelineBehavior<,>) });

            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
        }
    }
}
