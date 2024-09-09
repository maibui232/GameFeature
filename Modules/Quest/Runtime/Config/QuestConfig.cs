namespace Modules.Quest.Runtime.Config
{
    using ModuleConfig.Runtime;
#if UNITY_EDITOR
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using Modules.Quest.Runtime.Blueprint;
    using Modules.Quest.Runtime.Interface;
    using Newtonsoft.Json;
    using Sirenix.OdinInspector;
    using UnityEngine;
#endif

    public class QuestConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_QUEST";

#if UNITY_EDITOR
        [SerializeField, InlineButton("LoadOrCreate")]
        private string questId;

        [SerializeField, ShowIf("CanEdit")] private QuestConfigEditor questConfigEditor;

        private bool CanEdit
        {
            get
            {
                if (this.questConfigEditor == null) return false;

                return !string.IsNullOrEmpty(this.questConfigEditor.QuestId);
            }
        }

        private void LoadOrCreate()
        {
        }

        [Button(ButtonSizes.Gigantic), ShowIf("CanEdit")]
        private void ConvertToCsvData()
        {
            this.AppendTextToBlueprintFile(typeof(QuestBlueprint), this.questConfigEditor.ConvertToCsvData());
        }

        protected override void CreateBlueprint()
        {
            this.CreateCsvFile(typeof(QuestBlueprint));
        }

        private Dictionary<string, Type> nameToQuestConditionMap;
        private Dictionary<string, Type> nameToQuestRewardMap;

        private void OnEnable()
        {
            this.nameToQuestConditionMap = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestCondition>()
               .ToDictionary(type => type.Name, type => type);
            this.nameToQuestRewardMap = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestReward>()
               .ToDictionary(type => type.Name, type => type);
        }

#endif
    }

#if UNITY_EDITOR
    [Serializable]
    public class QuestConfigEditor
    {
        [SerializeField, BoxGroup("Config")] private string questId;
        [SerializeField, BoxGroup("Config")] private string questName;
        [SerializeField, BoxGroup("Config")] private string questDescription;

        // Condition
        [ShowInInspector, TabGroup("Condition")]
        private List<IQuestCondition> questConditions = new();

        // Reward
        [ShowInInspector, TabGroup("Reward")] private List<IQuestReward> questRewards = new();

        public string                QuestId          => this.questId;
        public string                QuestName        => this.questName;
        public string                QuestDescription => this.questDescription;
        public List<IQuestCondition> QuestConditions  => this.questConditions;
        public List<IQuestReward>    QuestRewards     => this.questRewards;

        public QuestConfigEditor(string questId)
        {
            this.questId = questId;
        }

        public QuestConfigEditor
        (
            string                questId,
            string                questName,
            string                questDescription,
            List<IQuestReward>    questRewards,
            List<IQuestCondition> questConditions
        )
        {
            this.questId          = questId;
            this.questName        = questName;
            this.questDescription = questDescription;
            this.questRewards     = questRewards;
            this.questConditions  = questConditions;
        }

        private string ConvertListDataToJsonData<T>(List<T> inputData)
        {
            var data  = string.Empty;
            var index = 0;
            foreach (var input in inputData)
            {
                data = $"{input.GetType().Name}|{JsonConvert.SerializeObject(input)}";
                if (index != inputData.Count - 1)
                {
                    data += ",";
                }

                index++;
            }

            if (data.Contains("\""))
            {
                data = data.Replace("\"", "\"\"");
            }

            if (data.Contains(",") || data.Contains("\"\""))
            {
                data = $"\"{data}\"";
            }

            return data;
        }

        public string ConvertToCsvData()
        {
            var text = $"{this.questId},"                                         +
                       $"{this.questName},"                                       +
                       $"{this.questDescription},"                                +
                       $"{this.ConvertListDataToJsonData(this.questConditions)}," +
                       $"{this.ConvertListDataToJsonData(this.questRewards)}";

            return text;
        }
    }
#endif
}