namespace Modules.Quest.Core.Runtime.Blueprint
{
    using System.Collections.Generic;
    using Modules.Quest.Core.Runtime.Model;
    using Modules.Quest.Core.Runtime.Model.Condition;
    using Modules.Quest.Core.Runtime.Model.Progress;
    using Modules.Quest.Core.Runtime.Model.Reward;
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("QuestBlueprint")]
    public class QuestBlueprint : GenericBlueprintByRow<string, QuestRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class QuestRecord
    {
        [OdinSerialize] public string                Id           { get; set; }
        [OdinSerialize] public string                Name         { get; set; }
        [OdinSerialize] public string                Description  { get; set; }
        [OdinSerialize] public QuestOperate          QuestOperate { get; set; }
        [OdinSerialize] public List<IQuestProgress>  Progresses   { get; set; }
        [OdinSerialize] public List<IQuestCondition> Conditions   { get; set; }
        [OdinSerialize] public List<IQuestReward>    Rewards      { get; set; }
    }
}