namespace Modules.BattlePass.Runtime.Blueprint
{
    using Services.BlueprintFlow.BlueprintReader;

    [BlueprintReader("BattlePassLevel")]
    public class BattlePassLevelBlueprint : GenericBlueprintReaderByRow<BattlePassType, BattlePassRecord>
    {
    }

    [CsvHeaderKey("BattlePassType")]
    public class BattlePassRecord
    {
        public BattlePassType                             BattlePassType { get; set; }
        public BlueprintByRow<int, BattlePassLevelRecord> LevelRecord    { get; set; }
    }

    [CsvHeaderKey("Level")]
    public class BattlePassLevelRecord
    {
        public int                                                 Level             { get; set; }
        public int                                                 Experience        { get; set; }
        public BlueprintByRow<string, BattlePassLevelRewardRecord> LevelRewardRecord { get; set; }
    }

    [CsvHeaderKey("RewardId")]
    public class BattlePassLevelRewardRecord
    {
        public string RewardId    { get; set; }
        public int    RewardValue { get; set; }
        public string RewardIcon  { get; set; }
    }

    public enum BattlePassType
    {
        Normal,
        Elite
    }
}