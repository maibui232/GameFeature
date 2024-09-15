namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.BlueprintFlow.BlueprintReader;

    [BlueprintReader("ItemDataBlueprint")]
    public class ItemDataBlueprint : GenericBlueprintReaderByRow<string, ItemDataRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class ItemDataRecord
    {
        public string Id              { get; set; }
        public string Name            { get; set; }
        public string Description     { get; set; }
        public string Category        { get; set; }
        public string IconAddressable { get; set; }
        public bool   IsDefault       { get; set; }
    }
}