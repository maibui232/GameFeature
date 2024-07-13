namespace Feature.Modules.SoundSetting
{
    using GameCore.Services.UserData.Interface;

    public class GeneralSoundSettingUserData : IUserData
    {
        public string LocaleId    { get; internal set; }
        public float  SoundVolume { get; internal set; }
        public float  MusicVolume { get; internal set; }
        public bool   IsVibrateOn { get; internal set; }

        public void Init()
        {
            this.SoundVolume = 1f;
            this.MusicVolume = 1f;
            this.IsVibrateOn = true;
        }
    }
}