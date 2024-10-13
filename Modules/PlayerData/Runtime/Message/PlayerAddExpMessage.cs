namespace Modules.PlayerData.Runtime.Message
{
    public struct PlayerAddExpMessage
    {
        public int Exp { get; }

        public PlayerAddExpMessage(int exp)
        {
            this.Exp = exp;
        }
    }
}