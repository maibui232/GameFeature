namespace Modules.Quest.Runtime.Config
{
    using Cysharp.Threading.Tasks;
    using ModuleConfig.Runtime;
    using Services.Blueprint.ReaderFlow;
#if UNITY_EDITOR
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using Modules.Quest.Runtime.Blueprint;
    using Modules.Quest.Runtime.Interface;
    using Sirenix.OdinInspector;
    using UnityEngine;
#endif

    public class QuestConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_QUEST";

#if UNITY_EDITOR

        private QuestBlueprint questBlueprint;
        private bool           CanSave => this.questBlueprint != null;

        [ShowInInspector, PropertyOrder(1)] private Dictionary<string, QuestRecordEditor> idToQuestRecordEditor = new();

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
                this.questBlueprint.Add(key, QuestRecordExtensions.ToQuestRecord(record));
            }

            EditorBlueprintReader.SaveBlueprint(this.questBlueprint).Forget();
        }

        protected override void CreateBlueprint()
        {
            this.CreateCsvFile(typeof(QuestBlueprint));
        }

#endif
#if UNITY_EDITOR
        [Serializable]
        private class QuestRecordEditor
        {
            [ShowInInspector, TabGroup("Quest")] internal string Id          { get; set; }
            [ShowInInspector, TabGroup("Quest")] internal string Name        { get; set; }
            [ShowInInspector, TabGroup("Quest")] internal string Description { get; set; }

#region Condition

            [ShowInInspector, ValueDropdown("ConditionTypeNames"), LabelText("Type"), InlineButton("AddCondition"), TabGroup("Conditions")]
            private string conditionTypeName;

            [ShowInInspector, TabGroup("Conditions")]
            internal List<IQuestCondition> Conditions { get; set; }

            private string[] ConditionTypeNames() => QuestEditorHelper.NameToConditionTypeCache.Keys.ToArray();

            private void AddCondition()
            {
                if (string.IsNullOrEmpty(this.conditionTypeName)) return;
                var type = QuestEditorHelper.NameToConditionTypeCache[this.conditionTypeName];
                this.Conditions.Add((IQuestCondition)ReflectionExtensions.CreateInstanceWithDefaultParams(type));
            }

#endregion

#region Reward

            [ShowInInspector, ValueDropdown("RewardTypeNames"), LabelText("Type"), InlineButton("AddReward"), TabGroup("Rewards")]
            private string rewardTypeName;

            [ShowInInspector, TabGroup("Rewards")] internal List<IQuestReward> Rewards { get; set; }

            private string[] RewardTypeNames() => QuestEditorHelper.NameToRewardTypeCache.Keys.ToArray();

            private void AddReward()
            {
                if (string.IsNullOrEmpty(this.rewardTypeName)) return;
                var type = QuestEditorHelper.NameToRewardTypeCache[this.rewardTypeName];
                this.Rewards.Add((IQuestReward)ReflectionExtensions.CreateInstanceWithDefaultParams(type));
            }

#endregion

            public QuestRecordEditor(string id, string name, string description, List<IQuestCondition> conditions, List<IQuestReward> rewards)
            {
                this.Id          = id;
                this.Name        = name;
                this.Description = description;
                this.Conditions  = conditions;
                this.Rewards     = rewards;
            }

            public static implicit operator QuestRecordEditor(QuestRecord record)
            {
                return new QuestRecordEditor(record.Id, record.Name, record.Description, record.Conditions, record.Rewards);
            }
        }

        private static class QuestRecordExtensions
        {
            public static QuestRecord ToQuestRecord(QuestRecordEditor recordEditor)
            {
                var record = new QuestRecord();
                foreach (var propInfo in recordEditor.GetType().GetProperties())
                {
                    record.GetType().GetProperty(propInfo.Name)?.SetValue(record, propInfo.GetValue(recordEditor));
                }

                return record;
            }
        }

        private static class QuestEditorHelper
        {
            public static readonly Dictionary<string, Type> NameToConditionTypeCache;
            public static readonly Dictionary<string, Type> NameToRewardTypeCache;

            static QuestEditorHelper()
            {
                NameToConditionTypeCache = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestCondition>().ToDictionary(type => type.Name, type => type);
                NameToRewardTypeCache    = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestReward>().ToDictionary(type => type.Name, type => type);
            }
        }
#endif
    }
}