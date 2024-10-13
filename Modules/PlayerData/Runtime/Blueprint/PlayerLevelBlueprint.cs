namespace Modules.PlayerData.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("PlayerLevelBlueprint")]
    public class PlayerLevelBlueprint : GenericBlueprintByRow<int, PlayerLevelRecord>
    {
    }

    [CsvHeaderKey("Level")]
    public class PlayerLevelRecord
    {
        [OdinSerialize] public int                                    Level      { get; set; }
        [OdinSerialize] public int                                    Experience { get; set; }
        [OdinSerialize] public BlueprintByRow<string, StatDataRecord> BaseStats  { get; set; }
    }

    [CsvHeaderKey("StatId")]
    public class StatDataRecord
    {
        [OdinSerialize] public string StatId { get; set; }
        [OdinSerialize] public int    Value  { get; set; }
    }
}