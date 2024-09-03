namespace Modules.CommonUI.Runtime.Popup
{
    using Cysharp.Threading.Tasks;
    using GameCore.Attribute;
    using GameCore.Services.Message;
    using GameCore.Services.ScreenFlow.Base.Screen;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class NotificationPopupView : BaseScreenView
    {
        [SerializeField] private TMP_Text titleTxt;
        [SerializeField] private TMP_Text contextTxt;
        [SerializeField] private Button   closeBtn;

        public TMP_Text TitleTxt   => this.titleTxt;
        public TMP_Text ContextTxt => this.contextTxt;
        public Button   CloseBtn   => this.closeBtn;
    }

    public class NotificationPopupModel
    {
        public string Title   { get; }
        public string Content { get; }

        public NotificationPopupModel(string title, string content)
        {
            this.Title   = title;
            this.Content = content;
        }
    }

    [ScreenInfo(nameof(NotificationPopupView), OrderLayer.Layer3)]
    public class NotificationPopupPresenter : BaseScreenPresenter<NotificationPopupView, NotificationPopupModel>
    {
        public NotificationPopupPresenter(IMessageService messageService) : base(messageService)
        {
        }

        protected override async UniTask OnViewInitAsync()
        {
            await base.OnViewInitAsync();
            this.View.CloseBtn.onClick.AddListener(this.CloseView);
        }

        public override UniTask BindData(NotificationPopupModel model)
        {
            this.View.TitleTxt.text   = model.Title;
            this.View.ContextTxt.text = model.Content;

            return UniTask.CompletedTask;
        }
    }
}