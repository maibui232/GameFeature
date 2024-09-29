namespace Modules.BattlePass.Runtime
{
    using ModuleConfig.Runtime;
    using Modules.BattlePass.Runtime.Blueprint;
    using Modules.BattlePass.Runtime.Config;
    using Modules.BattlePass.Runtime.UserData;

    public interface IBattlePassController
    {
        string                      CurrentId  { get; }
        int                         Level      { get; }
        int                         Experience { get; }
        BattlePassLevelRecord       GetLevelRecord(BattlePassType       battlePassType, int level);
        BattlePassLevelRewardRecord GetLevelRewardRecord(BattlePassType battlePassType, int level, string rewardId);
    }

    public class BattlePassController : IBattlePassController
    {
#region Inject

        private readonly IModuleConfigManager     moduleConfigManager;
        private readonly BattlePassUserData       battlePassUserData;
        private readonly BattlePassLevelBlueprint battlePassLevelBlueprint;

#endregion

        public BattlePassController
        (
            IModuleConfigManager     moduleConfigManager,
            BattlePassUserData       battlePassUserData,
            BattlePassLevelBlueprint battlePassLevelBlueprint
        )
        {
            this.moduleConfigManager      = moduleConfigManager;
            this.battlePassUserData       = battlePassUserData;
            this.battlePassLevelBlueprint = battlePassLevelBlueprint;
        }

        public string CurrentId => this.moduleConfigManager.GetModuleConfig<BattlePassConfig>().BattlePassId;

        public int Level => this.battlePassUserData.TryGetLevelData(this.CurrentId, out var levelData, out _)
                                ? levelData.Level
                                : 1;

        public int Experience => this.battlePassUserData.TryGetLevelData(this.CurrentId, out var levelData, out _)
                                     ? levelData.Experience
                                     : 0;

        public BattlePassLevelRecord GetLevelRecord(BattlePassType battlePassType, int level)
        {
            var battlePassLevels = this.battlePassLevelBlueprint.GetRecordByKey(battlePassType);

            return battlePassLevels.LevelRecord[level];
        }

        public BattlePassLevelRewardRecord GetLevelRewardRecord(BattlePassType battlePassType, int level, string rewardId)
        {
            var levelRecord = this.GetLevelRecord(battlePassType, level);

            return levelRecord?.LevelRewardRecord[rewardId];
        }
    }
}