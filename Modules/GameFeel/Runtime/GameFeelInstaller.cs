namespace Modules.GameFeel.Runtime
{
    using Modules.GameFeel.Runtime.VfxAttractor;
    using VContainer;
    using VContainer.Unity;
    using VContainerProvider.Scope.Installer;

    public class GameFeelInstaller : Installer<GameFeelInstaller, VfxAttractorService>
    {
        private readonly VfxAttractorService vfxAttractorService;

        public GameFeelInstaller(VfxAttractorService vfxAttractorService)
        {
            this.vfxAttractorService = vfxAttractorService;
        }

        public override void InstallBinding(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(this.vfxAttractorService, Lifetime.Singleton).DontDestroyOnLoad().AsImplementedInterfaces();
        }
    }
}