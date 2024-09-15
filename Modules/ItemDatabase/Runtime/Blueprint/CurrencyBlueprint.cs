namespace Modules.ItemDatabase.Runtime.Blueprint
{
    using Services.BlueprintFlow.BlueprintReader;

    [BlueprintReader("CurrencyBlueprint")]
    public class CurrencyBlueprint : GenericBlueprintReaderByRow<string, CurrencyRecord>
    {
    }

    [CsvHeaderKey("Id")]
    public class CurrencyRecord
    {
        public string Id              { get; set; }
        public string Name            { get; set; }
        public int    Min             { get; set; }
        public int    Max             { get; set; }
        public string IconAddressable { get; set; }
        public string VfxAddressable  { get; set; }
    }
}