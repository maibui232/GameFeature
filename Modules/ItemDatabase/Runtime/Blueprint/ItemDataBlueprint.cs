namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("ItemDataBlueprint")]
    public class ItemDataBlueprint : GenericBlueprintByRow<string, ItemDataRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class ItemDataRecord
    {
        [OdinSerialize] public string Id              { get; set; }
        [OdinSerialize] public string Name            { get; set; }
        [OdinSerialize] public string Description     { get; set; }
        [OdinSerialize] public string Category        { get; set; }
        [OdinSerialize] public string IconAddressable { get; set; }
        [OdinSerialize] public bool   IsDefault       { get; set; }
    }
}