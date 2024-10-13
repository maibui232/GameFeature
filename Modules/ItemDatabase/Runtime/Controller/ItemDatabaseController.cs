namespace Modules.ItemDatabase.Runtime.Controller
{
    using System;
    using Modules.ItemDatabase.Runtime.Blueprint;
    using Modules.ItemDatabase.Runtime.Message;
    using Modules.ItemDatabase.Runtime.UserData;
    using Services.Message;

    public interface IItemDatabaseController
    {
        void BuyItem(string itemId, Action<CurrencyCallbackData> onCompleted, Action<CurrencyCallbackData> onFailed);

        ShopItemDataRecord GetShopItemDataRecord(string itemId);
        ItemDataRecord     GetItemDataRecord(string     itemId);
        ItemData           GetItemData(string           itemId, ItemStatus initialStatus = ItemStatus.Locked);
        void               UnlockedItem(string          itemId);
        void               OwnedItem(string             itemId);
    }

    public class ItemDatabaseController : IItemDatabaseController
    {
#region Inject

        private readonly IMessageService        messageService;
        private readonly ItemDatabaseUserData   itemDatabaseUserData;
        private readonly ItemDataBlueprint      itemDataBlueprint;
        private readonly ShopItemDataBlueprint  shopItemDataBlueprint;
        private readonly CurrencyDataController currencyDataController;

#endregion

        public ItemDatabaseController
        (
            IMessageService        messageService,
            ItemDatabaseUserData   itemDatabaseUserData,
            ItemDataBlueprint      itemDataBlueprint,
            ShopItemDataBlueprint  shopItemDataBlueprint,
            CurrencyDataController currencyDataController
        )
        {
            this.messageService         = messageService;
            this.itemDatabaseUserData   = itemDatabaseUserData;
            this.itemDataBlueprint      = itemDataBlueprint;
            this.shopItemDataBlueprint  = shopItemDataBlueprint;
            this.currencyDataController = currencyDataController;
        }

        public void BuyItem(string itemId, Action<CurrencyCallbackData> onCompleted, Action<CurrencyCallbackData> onFailed)
        {
            var shopItemRecord = this.GetShopItemDataRecord(itemId);
            this.currencyDataController.AddCurrency(shopItemRecord.BuyMethodId, -shopItemRecord.Price, OnCompleted, OnFailed);

            return;

            void OnCompleted(CurrencyCallbackData data)
            {
                onCompleted?.Invoke(data);
            }

            void OnFailed(CurrencyCallbackData data)
            {
                onFailed?.Invoke(data);
            }
        }

        public ShopItemDataRecord GetShopItemDataRecord(string itemId)
        {
            return this.shopItemDataBlueprint.GetRecordByKey(itemId);
        }

        public ItemDataRecord GetItemDataRecord(string itemId)
        {
            return this.itemDataBlueprint.GetRecordByKey(itemId);
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

        public void UnlockedItem(string itemId)
        {
            this.SetItemStatus(itemId, ItemStatus.Unlocked);
        }

        public void OwnedItem(string itemId)
        {
            this.SetItemStatus(itemId, ItemStatus.Owned);
        }

        private void SetItemStatus(string itemId, ItemStatus status)
        {
            this.messageService.Publish(new ItemStatusChangedMessage(itemId, status));
            var itemData = this.GetItemData(itemId, ItemStatus.Unlocked);
            itemData.ItemStatus = status;
        }
    }
}