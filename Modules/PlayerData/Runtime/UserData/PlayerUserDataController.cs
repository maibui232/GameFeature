namespace Modules.PlayerData.Runtime.UserData
{
    using Modules.PlayerData.Runtime.Blueprint;
    using Modules.PlayerData.Runtime.Message;
    using Services.Message;

    public interface IPlayerUserDataController
    {
        int               Level              { get; }
        PlayerLevelRecord CurrentLevelRecord { get; }
        void              AddExp(int exp);
    }

    public class PlayerUserDataController : IPlayerUserDataController
    {
#region Inject

        private readonly IMessageService      messageService;
        private readonly PlayerUserData       playerUserData;
        private readonly PlayerLevelBlueprint playerLevelBlueprint;

#endregion

        public PlayerUserDataController
        (
            IMessageService      messageService,
            PlayerUserData       playerUserData,
            PlayerLevelBlueprint playerLevelBlueprint
        )
        {
            this.messageService       = messageService;
            this.playerUserData       = playerUserData;
            this.playerLevelBlueprint = playerLevelBlueprint;
        }

        public int Level => this.playerUserData.Level;

        public PlayerLevelRecord CurrentLevelRecord => this.playerLevelBlueprint.GetRecordByKey(this.Level);

        public void AddExp(int exp)
        {
            this.messageService.Publish(new PlayerAddExpMessage(exp));
            this.playerUserData.CurrentExp += exp;
            var currentLevelRecord = this.CurrentLevelRecord;

            if (this.playerUserData.CurrentExp < currentLevelRecord.Experience) return;

            this.LevelUp();
            this.playerUserData.CurrentExp -= currentLevelRecord.Experience;
        }

        private void LevelUp()
        {
            this.playerUserData.Level++;
            this.messageService.Publish(new PlayerLevelUpMessage(this.playerUserData.Level));
        }
    }
}