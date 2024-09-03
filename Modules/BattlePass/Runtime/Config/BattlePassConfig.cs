namespace Modules.BattlePass.Runtime.Config
{
    using ModuleConfig.Runtime;
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
            const string blueprintHeader = "BattlePassType,Level,Experience,RewardId,RewardValue,RewardIcon";
            this.CreateCsvFile("BattlePassLevel", blueprintHeader);
        }
#endif
    }
}