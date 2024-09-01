namespace Feature.Modules.BattlePass.Runtime.Config
{
    using ModuleConfig.Runtime;

    public class BattlePassConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_BATTLE_PASS";
    }
}