namespace Modules.Quest.Core.Runtime.Model.Condition
{
    using System;

    public interface IQuestCondition
    {
        Type HandlerType { get; }

        public interface IHandler<in T> where T : IQuestCondition
        {
            bool IsAvailable(IQuestCondition condition);
        }
    }
}