namespace Modules.BattlePass.Runtime.UserData
{
    using System.Collections.Generic;
    using GameCore.Services.UserData.Interface;

    public class BattlePassUserData : IUserData
    {
        public Dictionary<string, BattlePassLevel> IdToLevelData { get; set; }

        public bool TryGetLevelData(string id, out BattlePassLevel levelData, out string result)
        {
            if (this.IdToLevelData.TryGetValue(id, out levelData))
            {
                result = "Success";

                return true;
            }

            result = $"Unknown Battle Pass Id: {id}";

            return false;
        }

        public bool TryCreateBattlePassLevel(string id, out string result)
        {
            if (string.IsNullOrEmpty(id))
            {
                result = "Invalid Battle Pass Id";

                return false;
            }

            if (this.IdToLevelData.ContainsKey(id))
            {
                result = $"Already Exists: {id}";

                return false;
            }

            this.IdToLevelData.Add(id, new BattlePassLevel());

            result = "Success";

            return true;
        }

        public void Init()
        {
        }
    }

    public class BattlePassLevel
    {
        public int Level      { get; set; } = 1;
        public int Experience { get; set; } = 0;
    }
}