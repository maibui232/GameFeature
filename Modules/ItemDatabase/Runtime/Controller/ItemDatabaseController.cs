namespace Modules.ItemDatabase.Runtime.Controller
{
    using System;
    using Modules.ItemDatabase.Runtime.Blueprint;
    using Modules.ItemDatabase.Runtime.UserData;

    public interface IItemDatabaseController
    {
        void BuyItem(int itemId, Action<string> onCompleted, Action<string> onFailed);

        ShopItemDataRecord GetShopItemDataRecord(string itemId);
        ItemDataRecord     GetItemDataRecord(string     itemId);
        ItemData           GetItemData(string           itemId, ItemStatus initialStatus = ItemStatus.Locked);
    }

    public class ItemDatabaseController : IItemDatabaseController
    {
#region Inject

        private readonly ItemDatabaseUserData  itemDatabaseUserData;
        private readonly ItemDataBlueprint     itemDataBlueprint;
        private readonly ShopItemDataBlueprint shopItemDataBlueprint;
        private readonly CurrencyBlueprint     currencyBlueprint;

#endregion

        public ItemDatabaseController
        (
            ItemDatabaseUserData  itemDatabaseUserData,
            ItemDataBlueprint     itemDataBlueprint,
            ShopItemDataBlueprint shopItemDataBlueprint,
            CurrencyBlueprint     currencyBlueprint
        )
        {
            this.itemDatabaseUserData  = itemDatabaseUserData;
            this.itemDataBlueprint     = itemDataBlueprint;
            this.shopItemDataBlueprint = shopItemDataBlueprint;
            this.currencyBlueprint     = currencyBlueprint;
        }

        public void BuyItem(int itemId, Action<string> onCompleted, Action<string> onFailed)
        {
        }

        public ShopItemDataRecord GetShopItemDataRecord(string itemId)
        {
            return this.shopItemDataBlueprint.GetDataById(itemId);
        }

        public ItemDataRecord GetItemDataRecord(string itemId)
        {
            return this.itemDataBlueprint.GetDataById(itemId);
        }

        public ItemData GetItemData(string itemId, ItemStatus initialStatus = ItemStatus.Locked)
        {
            if (this.itemDatabaseUserData.IdToItemData.TryGetValue(itemId, out var itemData))
            {
                CheckReferenceRecord(itemData);

                return itemData;
            }

            itemData = new ItemData(itemId, initialStatus);
            CheckReferenceRecord(itemData);
            this.itemDatabaseUserData.IdToItemData.Add(itemId, itemData);

            return itemData;

            void CheckReferenceRecord(ItemData data)
            {
                if (data.ShopItemDataRecord != null && data.ItemDataRecord != null) return;
                data.ShopItemDataRecord = this.GetShopItemDataRecord(itemId);
                data.ItemDataRecord     = this.GetItemDataRecord(itemId);
            }
        }
    }
}