namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.BlueprintFlow.BlueprintReader;

    [BlueprintReader("ItemCategoryBlueprint")]
    public class ItemCategoryBlueprint : GenericBlueprintReaderByRow<string, ItemCategoryRecord>
    {
    }

    [CsvHeaderKey("Category")]
    public class ItemCategoryRecord
    {
        public string Category        { get; set; }
        public string IconAddressable { get; set; }
    }
}