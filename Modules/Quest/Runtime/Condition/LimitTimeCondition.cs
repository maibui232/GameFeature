namespace Modules.Quest.Runtime.Condition
{
    using System;
    using Modules.Quest.Runtime.Interface;
    using Sirenix.OdinInspector;

    public class LimitTimeCondition : IQuestCondition
    {
        [ShowInInspector] public DateTime StartTime { get; private set; }
        [ShowInInspector] public DateTime EndTime   { get; private set; }
    }
}