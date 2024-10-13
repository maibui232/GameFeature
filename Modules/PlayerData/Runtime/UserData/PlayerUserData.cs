namespace Modules.PlayerData.Runtime.UserData
{
    using Services.UserData.Interface;

    public class PlayerUserData : IUserData
    {
        public int CurrentExp { get; set; }
        public int Level      { get; set; }

        public void Init()
        {
            this.CurrentExp = 0;
            this.Level      = 1;
        }
    }
}