namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using Sirenix.Serialization;

    [CsvReader("ShopItemDataBlueprint")]
    public class ShopItemDataBlueprint : GenericBlueprintByRow<string, ShopItemDataRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class ShopItemDataRecord
    {
        [OdinSerialize] public string Id          { get; set; }
        [OdinSerialize] public string BuyMethodId { get; set; }
        [OdinSerialize] public int    Price       { get; set; }
    }
}