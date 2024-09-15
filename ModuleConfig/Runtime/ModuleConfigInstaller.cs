namespace ModuleConfig.Runtime
{
    using System.Collections.Generic;
    using VContainer;
    using VContainerProvider.Scope.Installer;

    public class ModuleConfigInstaller : Installer<ModuleConfigInstaller, List<BaseModuleConfig>>
    {
        private readonly List<BaseModuleConfig> moduleConfigs;

        public ModuleConfigInstaller(List<BaseModuleConfig> moduleConfigs)
        {
            this.moduleConfigs = moduleConfigs;
        }

        public override void InstallBinding(IContainerBuilder builder)
        {
            foreach (var config in this.moduleConfigs)
            {
                builder.RegisterInstance(config).AsSelf();
            }

            builder.Register<ModuleConfigManager>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}