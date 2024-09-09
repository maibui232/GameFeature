namespace Modules.Quest.Runtime.Condition
{
    using Modules.Quest.Runtime.Interface;
    using Sirenix.OdinInspector;

    public class PlayerLevelCondition : IQuestCondition
    {
        [ShowInInspector] public int Level { get; private set; }
    }
}