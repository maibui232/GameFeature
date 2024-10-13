namespace Modules.ItemDatabase.Runtime
{
    using Modules.ItemDatabase.Runtime.Controller;
    using Modules.ItemDatabase.Runtime.Message;
    using VContainer;
    using VContainerProvider;
    using VContainerProvider.Scope.Installer;

    public class ItemDatabaseInstaller : Installer<ItemDatabaseInstaller>
    {
        public override void InstallBinding(IContainerBuilder builder)
        {
            builder.RegisterMessage<CollectCurrencyMessage>();
            builder.RegisterMessage<SpendCurrencyMessage>();

            builder.Register<CurrencyDataController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ItemDatabaseController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}