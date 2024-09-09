namespace Modules.ItemDatabase.Runtime.Config
{
    using ModuleConfig.Runtime;
    using Modules.ItemDatabase.Runtime.Blueprint;

    public class ItemDatabaseConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_ITEM_DATABASE";

#if UNITY_EDITOR
        protected override void CreateBlueprint()
        {
            base.CreateBlueprint();
            this.CreateCsvFile(typeof(ItemDataBlueprint));
            this.CreateCsvFile(typeof(ShopItemDataBlueprint));
            this.CreateCsvFile(typeof(ItemCategoryBlueprint));
            this.CreateCsvFile(typeof(CurrencyBlueprint));
        }
#endif
    }
}