namespace Modules.ItemDatabase.Runtime
{
    using GameCore.Extensions.VContainer.Installer;
    using Modules.ItemDatabase.Runtime.Controller;
    using VContainer;

    public class ItemDatabaseInstaller : Installer<ItemDatabaseInstaller>
    {
        public override void InstallBinding(IContainerBuilder builder)
        {
            builder.Register<CurrencyDataController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ItemDatabaseController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}