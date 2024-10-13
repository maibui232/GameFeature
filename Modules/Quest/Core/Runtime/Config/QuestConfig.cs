namespace Modules.Quest.Core.Runtime.Config
{
    using Cysharp.Threading.Tasks;
    using ModuleConfig.Runtime;
    using Modules.Quest.Core.Runtime.Blueprint;
    using Services.Blueprint.ReaderFlow;
    using Sirenix.OdinInspector;

    public class QuestConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_QUEST";

#if UNITY_EDITOR

        protected override void OnEnableChange(bool isEnable)
        {
            base.OnEnableChange(isEnable);
            if (isEnable)
            {
                this.LoadBlueprint<QuestBlueprint>().Forget();
            }
        }

        [ShowInInspector, PropertyOrder(1)] private QuestBlueprint questBlueprint;

        [ButtonGroup, Button(ButtonSizes.Gigantic), PropertyOrder(0)]
        private async void LoadBlueprint()
        {
            this.questBlueprint = await this.LoadBlueprint<QuestBlueprint>();
        }

        [ButtonGroup, Button(ButtonSizes.Gigantic), PropertyOrder(0)]
        private void SaveBlueprint()
        {
            EditorBlueprintReader.SaveBlueprint(this.questBlueprint).Forget();
        }
    }
#endif
}