namespace Modules.BattlePass.Runtime.Config
{
    using Cysharp.Threading.Tasks;
    using ModuleConfig.Runtime;
    using Modules.BattlePass.Runtime.Blueprint;
    using Services.Blueprint.ReaderFlow;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class BattlePassConfig : BaseModuleConfig
    {
        [SerializeField] private string battlePassId;

        public string BattlePassId => this.battlePassId;

        protected override string ScriptDefineSymbol => "FEATURE_BATTLE_PASS";

#if UNITY_EDITOR
        protected override void OnEnableChange(bool isEnable)
        {
            base.OnEnableChange(isEnable);
            if (isEnable)
            {
                this.LoadBlueprint<BattlePassLevelBlueprint>().Forget();
            }
        }

        [PropertyOrder(1)] [ShowInInspector] private BattlePassLevelBlueprint battlePassLevelBlueprint;

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private async void LoadBlueprint()
        {
            this.battlePassLevelBlueprint = await this.LoadBlueprint<BattlePassLevelBlueprint>();
        }

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private void SaveBlueprint()
        {
            EditorBlueprintReader.SaveBlueprint(this.battlePassLevelBlueprint).Forget();
        }
#endif
    }
}