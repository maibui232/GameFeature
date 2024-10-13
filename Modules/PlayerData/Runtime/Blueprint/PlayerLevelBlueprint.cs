namespace Modules.PlayerData.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;

    [CsvReader("PlayerLevelBlueprint")]
    public class PlayerLevelBlueprint : GenericBlueprintByRow<int, PlayerLevelRecord>
    {
    }

    [CsvHeaderKey("Level")]
    public class PlayerLevelRecord
    {
        public int                                    Level      { get; set; }
        public int                                    Experience { get; set; }
        public BlueprintByRow<string, StatDataRecord> BaseStats  { get; set; }
    }

    [CsvHeaderKey("StatId")]
    public class StatDataRecord
    {
        public string StatId { get; set; }
        public int    Value  { get; set; }
    }
}