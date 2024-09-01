namespace Feature.Modules.CommonUI.Runtime.Screen
{
    using Cysharp.Threading.Tasks;
    using GameCore.Services.BlueprintFlow.BlueprintControlFlow;
    using GameCore.Services.Message;
    using GameCore.Services.ScreenFlow.Base.Screen;
    using GameExtensions.Runtime.AsyncProgress;

    public abstract class LoadingScreenPresenter<TView> : BaseScreenPresenter<TView> where TView : BaseScreenView
    {
        private readonly BlueprintReaderManager blueprintReaderManager;

        protected LoadingScreenPresenter
        (
            IMessageService        messageService,
            BlueprintReaderManager blueprintReaderManager
        ) : base(messageService)
        {
            this.blueprintReaderManager = blueprintReaderManager;
        }

        private UniTaskProgressHandler asyncProgressHandler;

        protected UniTaskProgressHandler AsyncProgressHandler => this.asyncProgressHandler ??= AsyncProgressTracker.CreateHandler<UniTaskProgressHandler>();

        public override UniTask BindData()
        {
            this.AsyncProgressHandler.ProgressChangeEvent += this.OnLoading;
            this.AsyncProgressHandler.CompletedEvent      += this.OnCompleted;
            this.OnLoading(0);

            this.AsyncProgressHandler.TrackUniTask(this.blueprintReaderManager.LoadBlueprint());

            return UniTask.CompletedTask;
        }

        protected virtual void OnLoading(float progress)
        {
        }

        protected virtual void OnCompleted()
        {
        }
    }
}