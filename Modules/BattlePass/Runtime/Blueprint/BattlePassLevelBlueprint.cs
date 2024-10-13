namespace Modules.BattlePass.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("BattlePassLevel")]
    public class BattlePassLevelBlueprint : GenericBlueprintByRow<BattlePassType, BattlePassRecord>
    {
    }

    [CsvHeaderKey("BattlePassType")]
    public class BattlePassRecord
    {
        [OdinSerialize] public BattlePassType                             BattlePassType { get; set; }
        [OdinSerialize] public BlueprintByRow<int, BattlePassLevelRecord> LevelRecord    { get; set; }
    }

    [CsvHeaderKey("Level")]
    public class BattlePassLevelRecord
    {
        [OdinSerialize] public int                                                 Level       { get; set; }
        [OdinSerialize] public int                                                 Experience  { get; set; }
        [OdinSerialize] public BlueprintByRow<string, BattlePassLevelRewardRecord> IdToRewards { get; set; }
    }

    [CsvHeaderKey("RewardId")]
    public class BattlePassLevelRewardRecord
    {
        [OdinSerialize] public string RewardId    { get; set; }
        [OdinSerialize] public int    RewardValue { get; set; }
        [OdinSerialize] public string RewardIcon  { get; set; }
    }

    public enum BattlePassType
    {
        Normal,
        Elite
    }
}