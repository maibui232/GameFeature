namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("ItemCategoryBlueprint")]
    public class ItemCategoryBlueprint : GenericBlueprintByRow<string, ItemCategoryRecord>
    {
    }

    [CsvHeaderKey("Category")]
    public class ItemCategoryRecord
    {
        [OdinSerialize]public string Category        { get; set; }
        [OdinSerialize]public string IconAddressable { get; set; }
    }
}