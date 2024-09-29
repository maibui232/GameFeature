namespace Modules.Quest.Runtime.Blueprint
{
    using System.Collections.Generic;
    using Modules.Quest.Runtime.Interface;
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;

    [CsvReader("QuestBlueprint")]
    public class QuestBlueprint : GenericBlueprintByRow<string, QuestRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class QuestRecord
    {
        public string                Id          { get; private set; }
        public string                Name        { get; private set; }
        public string                Description { get; private set; }
        public List<IQuestCondition> Conditions  { get; private set; }
        public List<IQuestReward>    Rewards     { get; private set; }
    }
}