namespace Modules.Quest.Runtime.Reward
{
    using Modules.Quest.Runtime.Interface;
    using Sirenix.OdinInspector;

    public class ItemReward : IQuestReward
    {
        [ShowInInspector] public string ItemId { get; private set; }
        [ShowInInspector] public int    Amount { get; private set; } = 1;

        public ItemReward(string itemId, int amount)
        {
            this.ItemId = itemId;
            this.Amount = amount;
        }
    }
}