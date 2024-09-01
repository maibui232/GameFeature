namespace Feature.Modules.CommonUI.Runtime.Config
{
    using ModuleConfig.Runtime;

    public class CommonUIConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_COMMON_UI";
    }
}