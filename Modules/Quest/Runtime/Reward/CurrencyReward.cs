namespace Modules.Quest.Runtime.Reward
{
    using Modules.Quest.Runtime.Interface;
    using Sirenix.OdinInspector;

    public class CurrencyReward : IQuestReward
    {
        [ShowInInspector] public string CurrencyId { get; private set; }
        [ShowInInspector] public int    Amount     { get; private set; } = 1;
    }
}