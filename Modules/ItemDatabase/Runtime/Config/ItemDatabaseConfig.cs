namespace Modules.ItemDatabase.Runtime.Config
{
    using Cysharp.Threading.Tasks;
    using ModuleConfig.Runtime;
    using Modules.ItemDatabase.Runtime.Blueprint;
    using Services.Blueprint.ReaderFlow;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class ItemDatabaseConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "FEATURE_ITEM_DATABASE";

#if UNITY_EDITOR

        protected override void OnEnableChange(bool isEnable)
        {
            base.OnEnableChange(isEnable);
            if (isEnable)
            {
                this.LoadBlueprint<CurrencyBlueprint>().Forget();
                this.LoadBlueprint<ItemCategoryBlueprint>().Forget();
                this.LoadBlueprint<ItemDataBlueprint>().Forget();
                this.LoadBlueprint<ShopItemDataBlueprint>().Forget();
            }
        }

        [ShowInInspector] [PropertyOrder(1)] [TabGroup("Currency")]
        private CurrencyBlueprint currencyBlueprint;

        [ShowInInspector] [PropertyOrder(1)] [TabGroup("ItemData")]
        private ItemDataBlueprint itemDataBlueprint;

        [ShowInInspector] [PropertyOrder(1)] [TabGroup("ItemCategory")]
        private ItemCategoryBlueprint itemCategoryBlueprint;

        [ShowInInspector] [PropertyOrder(1)] [TabGroup("ShopItemData")]
        private ShopItemDataBlueprint shopItemDataBlueprint;

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private async void LoadBlueprint()
        {
            this.currencyBlueprint     = await this.LoadBlueprint<CurrencyBlueprint>();
            this.itemDataBlueprint     = await this.LoadBlueprint<ItemDataBlueprint>();
            this.itemCategoryBlueprint = await this.LoadBlueprint<ItemCategoryBlueprint>();
            this.shopItemDataBlueprint = await this.LoadBlueprint<ShopItemDataBlueprint>();
        }

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private void SaveBlueprint()
        {
            EditorBlueprintReader.SaveBlueprint(this.currencyBlueprint).Forget();
            EditorBlueprintReader.SaveBlueprint(this.itemCategoryBlueprint).Forget();
            EditorBlueprintReader.SaveBlueprint(this.itemDataBlueprint).Forget();
            EditorBlueprintReader.SaveBlueprint(this.shopItemDataBlueprint).Forget();
        }
#endif
    }
}