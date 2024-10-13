namespace Modules.ItemDatabase.Runtime.Message
{
    using Modules.ItemDatabase.Runtime.UserData;

    public struct ItemStatusChangedMessage
    {
        public string ItemId { get; }

        public ItemStatusChangedMessage(string itemId, ItemStatus status)
        {
            this.ItemId = itemId;
        }
    }
}