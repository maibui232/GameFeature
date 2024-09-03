namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using GameCore.Services.BlueprintFlow.BlueprintReader;

    [BlueprintReader("ShopItemDataBlueprint")]
    public class ShopItemDataBlueprint : GenericBlueprintReaderByRow<string, ShopItemDataRecord>
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