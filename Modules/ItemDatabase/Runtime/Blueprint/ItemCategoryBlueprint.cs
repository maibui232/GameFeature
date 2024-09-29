namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;

    [CsvReader("ItemCategoryBlueprint")]
    public class ItemCategoryBlueprint : GenericBlueprintByRow<string, ItemCategoryRecord>
    {
    }

    [CsvHeaderKey("Category")]
    public class ItemCategoryRecord
    {
        public string Category        { get; set; }
        public string IconAddressable { get; set; }
    }
}