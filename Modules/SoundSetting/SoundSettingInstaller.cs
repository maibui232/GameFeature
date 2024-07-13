namespace Feature.Modules.SoundSetting
{
    using GameCore.Extensions.VContainer.Installer;
    using VContainer;

    public class SoundSettingInstaller : Installer<SoundSettingInstaller>
    {
        public override void InstallBinding(IContainerBuilder builder)
        {
            builder.Register<GeneralSoundSettingUserData>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}