namespace Modules.Quest.Core.Runtime.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Cysharp.Threading.Tasks;
    using GameExtensions.Runtime.Reflection;
    using ModuleConfig.Runtime;
    using Modules.Quest.Core.Runtime.Blueprint;
    using Modules.Quest.Core.Runtime.Model;
    using Modules.Quest.Core.Runtime.Model.Condition;
    using Modules.Quest.Core.Runtime.Model.Progress;
    using Modules.Quest.Core.Runtime.Model.Reward;
    using Services.Blueprint.ReaderFlow;
    using Sirenix.OdinInspector;

    public class QuestConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_QUEST";

#if UNITY_EDITOR

        private QuestBlueprint questBlueprint;
        private bool           CanSave => this.questBlueprint != null;

        [ShowInInspector, PropertyOrder(1)] private Dictionary<string, QuestRecord> idToQuestRecordEditor = new();

        [ButtonGroup, Button(ButtonSizes.Gigantic), PropertyOrder(0)]
        private async void LoadBlueprint()
        {
            this.questBlueprint = await EditorBlueprintReader.OpenReadBlueprint<QuestBlueprint>();
            foreach (var (key, record) in this.questBlueprint)
            {
                this.idToQuestRecordEditor.Add(key, record);
            }
        }

        [ButtonGroup, Button(ButtonSizes.Gigantic), PropertyOrder(0), ShowIf("CanSave")]
        private void SaveBlueprint()
        {
            this.questBlueprint.Clear();
            foreach (var (key, record) in this.idToQuestRecordEditor)
            {
                this.questBlueprint.Add(key, record);
            }

            EditorBlueprintReader.SaveBlueprint(this.questBlueprint).Forget();
        }

        protected override void CreateBlueprint()
        {
            this.CreateCsvFile(typeof(QuestBlueprint));
        }
    }
#endif
}