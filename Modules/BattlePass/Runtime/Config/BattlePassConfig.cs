namespace Modules.BattlePass.Runtime.Config
{
    using ModuleConfig.Runtime;
    using Modules.BattlePass.Runtime.Blueprint;
    using UnityEngine;

    public class BattlePassConfig : BaseModuleConfig
    {
        [SerializeField] private string battlePassId;

        public string BattlePassId => this.battlePassId;

        protected override string ScriptDefineSymbol => "FEATURE_BATTLE_PASS";

#if UNITY_EDITOR
        protected override void CreateBlueprint()
        {
            base.CreateBlueprint();
            this.CreateCsvFile(typeof(BattlePassLevelBlueprint));
        }
#endif
    }
}