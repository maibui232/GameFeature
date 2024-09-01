namespace ModuleConfig.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IModuleConfigManager
    {
        T GetModuleConfig<T>() where T : BaseModuleConfig;
    }

    public class ModuleConfigManager : IModuleConfigManager
    {
#region Inject

        private readonly Dictionary<Type, BaseModuleConfig> typeToModuleConfigs;

#endregion

        public ModuleConfigManager(IEnumerable<BaseModuleConfig> moduleConfigs)
        {
            this.typeToModuleConfigs = moduleConfigs.ToDictionary(config => config.GetType(), config => config);
        }

        public T GetModuleConfig<T>() where T : BaseModuleConfig
        {
            if (this.typeToModuleConfigs.TryGetValue(typeof(T), out var moduleConfig)) return moduleConfig as T;

            throw new Exception($"Module config type {typeof(T)} not found");
        }
    }
}