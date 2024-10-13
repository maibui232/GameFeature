namespace Modules.Quest.Core.Runtime.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Services.UserData.Interface;

    public class QuestUserData : IUserData
    {
        [JsonProperty] public Dictionary<string, QuestData> IdToQuestData { get; set; } = new();

        public void Init()
        {
        }
    }
}