namespace Modules.Quest.Core.Runtime.Model
{
    using System.Collections.Generic;
    using Modules.Quest.Core.Runtime.Blueprint;
    using Modules.Quest.Core.Runtime.Model.Condition;
    using Modules.Quest.Core.Runtime.Model.Progress;
    using Modules.Quest.Core.Runtime.Model.Reward;
    using Newtonsoft.Json;

    public class QuestData
    {
        [JsonProperty] public string               QuestId  { get; private set; }
        [JsonProperty] public QuestStatus          Status   { get; private set; }
        [JsonProperty] public List<IQuestProgress> Progress { get; private set; }

        public string                Name         { get; private set; }
        public string                Description  { get; private set; }
        public QuestOperate          QuestOperate { get; private set; }
        public List<IQuestCondition> Conditions   { get; private set; }
        public List<IQuestReward>    Rewards      { get; private set; }

        public QuestData(string questId, List<IQuestProgress> progresses)
        {
        }

        public void SetAdditionQuestData(QuestRecord questRecord)
        {
            this.Name         = questRecord.Name;
            this.Description  = questRecord.Description;
            this.QuestOperate = questRecord.QuestOperate;
            this.Conditions   = questRecord.Conditions;
            this.Rewards      = questRecord.Rewards;
        }
    }

    public enum QuestOperate
    {
        All,
        Any
    }

    public enum QuestStatus
    {
        Opened,
        InProgress,
        Completed,
        Closed,
        CanShow = Opened | InProgress | Completed,
    }
}