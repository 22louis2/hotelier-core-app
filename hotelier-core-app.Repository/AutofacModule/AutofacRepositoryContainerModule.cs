using Autofac;

namespace hotelier_core_app.Domain.AutofacModule
{
    public class AutofacRepositoryContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IAutoDependencyRepository).Assembly)
                .AssignableTo<IAutoDependencyRepository>()
                .As<IAutoDependencyRepository>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
