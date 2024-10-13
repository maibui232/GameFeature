namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("CurrencyBlueprint")]
    public class CurrencyBlueprint : GenericBlueprintByRow<string, CurrencyRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class CurrencyRecord
    {
        [OdinSerialize] public string Id              { get; set; }
        [OdinSerialize] public string Name            { get; set; }
        [OdinSerialize] public int    Min             { get; set; }
        [OdinSerialize] public int    Max             { get; set; }
        [OdinSerialize] public string IconAddressable { get; set; }
        [OdinSerialize] public string VfxAddressable  { get; set; }
    }
}