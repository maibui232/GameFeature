namespace Modules.PlayerData.Runtime.Message
{
    public struct PlayerLevelUpMessage
    {
        public int Level { get; }

        public PlayerLevelUpMessage(int level)
        {
            this.Level = level;
        }
    }
}