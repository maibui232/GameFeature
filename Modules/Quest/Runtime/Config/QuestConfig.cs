namespace Modules.Quest.Runtime.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using ModuleConfig.Runtime;
    using Modules.Quest.Runtime.Interface;
    using Newtonsoft.Json;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class QuestConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_QUEST";

#if UNITY_EDITOR
        [SerializeField, BoxGroup("Config")] private string questId;
        [SerializeField, BoxGroup("Config")] private string questName;
        [SerializeField, BoxGroup("Config")] private string questDescription;

#region Condition

        // Condition
        [ShowInInspector, TabGroup("Quest")] private List<IQuestCondition> questConditions = new();

        private Dictionary<string, Type> nameToQuestConditionMap = new();

        private string[] ConditionDropdown()
        {
            return this.nameToQuestConditionMap.Keys.ToArray();
        }

        private void CreateCondition()
        {
            if (string.IsNullOrEmpty(this.conditionType) || this.conditionType.Equals("Null")) return;
            var type = this.nameToQuestConditionMap[this.conditionType];
            this.questConditions.Add((IQuestCondition)Activator.CreateInstance(type));
        }

        [ShowInInspector, TabGroup("Quest"), InlineButton("CreateCondition"), ValueDropdown("ConditionDropdown")]
        private string conditionType;

#endregion

#region Reward

        // Reward
        [ShowInInspector, TabGroup("Reward")] private List<IQuestReward> questRewards = new();

        [SerializeField, TabGroup("Reward"), InlineButton("CreateReward"), ValueDropdown("RewardDropdown")]
        private string rewardType;

        private Dictionary<string, Type> nameToQuestRewardMap = new();

        private string[] RewardDropdown()
        {
            return this.nameToQuestRewardMap.Keys.ToArray();
        }

        private void CreateReward()
        {
            if (string.IsNullOrEmpty(this.rewardType) || this.rewardType.Equals("Null")) return;
            var type = this.nameToQuestRewardMap[this.rewardType];
            this.questRewards.Add((IQuestReward)Activator.CreateInstance(type));
        }

#endregion

        private void OnEnable()
        {
            this.nameToQuestConditionMap = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestCondition>()
               .ToDictionary(type => type.Name, type => type);
            this.nameToQuestRewardMap = AppDomain.CurrentDomain.GetAllTypeFromDerived<IQuestReward>()
               .ToDictionary(type => type.Name, type => type);
        }
#endif
    }
}