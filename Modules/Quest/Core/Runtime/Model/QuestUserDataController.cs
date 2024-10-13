namespace Modules.Quest.Core.Runtime.Model
{
    using System;
    using GameExtensions.Runtime.Collection;
    using Modules.Quest.Core.Runtime.Blueprint;
    using VContainer.Unity;

    public class QuestUserDataController : IInitializable
    {
#region Inject

        private readonly QuestUserData  questUserData;
        private readonly QuestBlueprint questBlueprint;

#endregion

        public QuestUserDataController
        (
            QuestUserData  questUserData,
            QuestBlueprint questBlueprint
        )
        {
            this.questUserData  = questUserData;
            this.questBlueprint = questBlueprint;
        }

        public QuestData GetOrAdd(string questId, Func<QuestData> newQuestData = null)
        {
            return this.questUserData.IdToQuestData.GetOrAdd(questId, newQuestData);
        }

        public void Initialize()
        {
            foreach (var (key, record) in this.questBlueprint)
            {
                var questData = this.GetOrAdd(key, () => new QuestData(key, record.Progresses));
                questData.SetAdditionQuestData(record);
            }
        }
    }
}