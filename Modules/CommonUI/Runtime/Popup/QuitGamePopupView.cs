namespace Modules.CommonUI.Runtime.Popup
{
    using Cysharp.Threading.Tasks;
    using Services.Message;
    using Services.ScreenFlow.Attribute;
    using Services.ScreenFlow.Base.Screen;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuitGamePopupView : BaseScreenView
    {
        [SerializeField] private Button quitBtn;
        [SerializeField] private Button closeBtn;

        public Button QuitBtn  => this.quitBtn;
        public Button CloseBtn => this.closeBtn;
    }

    [ScreenInfo(nameof(QuitGamePopupView), OrderLayer.Layer3)]
    public class QuitGamePopupPresenter : BaseScreenPresenter<QuitGamePopupView>
    {
        public QuitGamePopupPresenter(IMessageService messageService) : base(messageService)
        {
        }

        protected override async UniTask OnViewInitAsync()
        {
            await base.OnViewInitAsync();
            this.View.CloseBtn.onClick.AddListener(this.CloseView);
            this.View.QuitBtn.onClick.AddListener(this.OnClickQuit);
        }

        public override UniTask BindData()
        {
            return UniTask.CompletedTask;
        }

        private void OnClickQuit()
        {
            Application.Quit();
        }
    }
}