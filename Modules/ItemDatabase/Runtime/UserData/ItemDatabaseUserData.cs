namespace Modules.ItemDatabase.Runtime.UserData
{
    using System.Collections.Generic;
    using GameCore.Services.UserData.Interface;
    using Modules.ItemDatabase.Runtime.Blueprint;
    using Newtonsoft.Json;
    using Sirenix.Serialization;

    public class ItemDatabaseUserData : IUserData
    {
        [OdinSerialize] public Dictionary<string, string>   CategoryToItemUsing = new();
        [OdinSerialize] public Dictionary<string, ItemData> IdToItemData        = new();

        public void Init()
        {
        }
    }

    public class ItemData
    {
        public ItemData(string id, ItemStatus itemStatus)
        {
            this.Id         = id;
            this.ItemStatus = itemStatus;
        }

        public string     Id         { get; internal set; }
        public ItemStatus ItemStatus { get; internal set; }

        [JsonIgnore] public ItemDataRecord     ItemDataRecord     { get; internal set; }
        [JsonIgnore] public ShopItemDataRecord ShopItemDataRecord { get; internal set; }
    }

    public enum ItemStatus
    {
        Locked,
        Unlocked,
        Owned
    }
}