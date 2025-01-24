using Autofac;

namespace hotelier_core_app.Core.AutofacModule
{
    public class AutofacCoreContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyCore).Assembly)
                .AssignableTo<IAutoDependencyCore>()
                .As<IAutoDependencyCore>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
