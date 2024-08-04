namespace Feature.Modules.CommonUI.Runtime.Screen
{
    using Cysharp.Threading.Tasks;
    using GameCore.Services.Message;
    using GameCore.Services.ScreenFlow.Base.Screen;
    using GameExtensions.AsyncProgress;

    public abstract class LoadingScreenPresenter<TView> : BaseScreenPresenter<TView> where TView : BaseScreenView
    {
        protected LoadingScreenPresenter(IMessageService messageService) : base(messageService)
        {
        }

        private IAsyncProgressHandler asyncProgressHandler;

        protected IAsyncProgressHandler AsyncProgressHandler => this.asyncProgressHandler ??= AsyncProgressTracker.CreateHandler<UniTaskProgressHandler>();

        public override UniTask BindData()
        {
            this.asyncProgressHandler.ProgressChangeEvent += this.OnLoading;
            this.asyncProgressHandler.CompletedEvent      += this.OnCompleted;
            this.OnLoading(0);

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