namespace Modules.CommonUI.Runtime.Screen
{
    using Cysharp.Threading.Tasks;
    using GameExtensions.Runtime.AsyncProgress;
    using Services.BlueprintFlow.BlueprintControlFlow;
    using Services.Message;
    using Services.ScreenFlow.Base.Screen;

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