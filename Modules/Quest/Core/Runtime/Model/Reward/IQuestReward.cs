namespace Modules.Quest.Core.Runtime.Model.Reward
{
    using System;
    using Cysharp.Threading.Tasks;
    using Modules.Quest.Core.Runtime.Model.Condition;

    public interface IQuestReward
    {
        Type RewardHandlerType { get; }

        public interface IHandler<in T> where T : IQuestReward
        {
            UniTask Claim(IQuestReward reward);
        }
    }
}