namespace Modules.ItemDatabase.Runtime
{
    using Modules.ItemDatabase.Runtime.Controller;
    using VContainer;
    using VContainerProvider.Scope.Installer;

    public class ItemDatabaseInstaller : Installer<ItemDatabaseInstaller>
    {
        public override void InstallBinding(IContainerBuilder builder)
        {
            builder.Register<CurrencyDataController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ItemDatabaseController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}