namespace Modules.ItemDatabase.Runtime.Config
{
    using ModuleConfig.Runtime;

    public class ItemDatabaseConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_ITEM_DATABASE";

#if UNITY_EDITOR
        protected override void CreateBlueprint()
        {
            base.CreateBlueprint();
            this.CreateItemDataBlueprint();
            this.CreateShopItemDataBlueprint();
            this.CreateItemCategoryBlueprint();
            this.CreateCurrencyBlueprint();
        }

        private void CreateItemDataBlueprint()
        {
            const string header = "Id,Name,Description,Category,IconAddressable,IsDefault";
            this.CreateCsvFile("ItemDataBlueprint", header);
        }

        private void CreateShopItemDataBlueprint()
        {
            const string header = "Id,BuyMethodId,Price";
            this.CreateCsvFile("ShopItemDataBlueprint", header);
        }

        private void CreateItemCategoryBlueprint()
        {
            const string header = "Category,IconAddressable";
            this.CreateCsvFile("ItemCategoryBlueprint", header);
        }

        private void CreateCurrencyBlueprint()
        {
            const string header = "Id,Name,Min,Max,IconAddressable,VfxAddressable";
            this.CreateCsvFile("CurrencyBlueprint", header);
        }
#endif
    }
}