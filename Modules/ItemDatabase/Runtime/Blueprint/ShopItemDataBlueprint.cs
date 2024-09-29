namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow.GenericReader;

    [CsvReader("ShopItemDataBlueprint")]
    public class ShopItemDataBlueprint : GenericBlueprintByRow<string, ShopItemDataRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class ShopItemDataRecord
    {
        public string Id          { get; set; }
        public string BuyMethodId { get; set; }
        public int    Price       { get; set; }
    }
}