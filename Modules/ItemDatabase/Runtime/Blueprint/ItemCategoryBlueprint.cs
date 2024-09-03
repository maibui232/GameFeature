namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using GameCore.Services.BlueprintFlow.BlueprintReader;

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