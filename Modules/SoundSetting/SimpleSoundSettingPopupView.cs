namespace Feature.Modules.SoundSetting
{
    using Cysharp.Threading.Tasks;
    using Feature.Modules.CommonUI.Element;
    using GameCore.Attribute;
    using GameCore.Services.Message;
    using GameCore.Services.ScreenFlow.Base.Screen;
    using UnityEngine;
    using UnityEngine.UI;

    public class SimpleSoundSettingPopupView : BaseScreenView
    {
        [SerializeField] private OnOffButton soundBtn;
        [SerializeField] private OnOffButton musicBtn;
        [SerializeField] private OnOffButton vibrateBtn;
        [SerializeField] private Button      closeBtn;

        public OnOffButton SoundBtn   => this.soundBtn;
        public OnOffButton MusicBtn   => this.musicBtn;
        public OnOffButton VibrateBtn => this.vibrateBtn;
        public Button      CloseBtn   => this.closeBtn;
    }

    [ScreenInfo(nameof(SimpleSoundSettingPopupView), OrderLayer.Overlay)]
    public class SimpleSettingPopupPresenter : BaseScreenPresenter<SimpleSoundSettingPopupView>
    {
#region Inject

        private readonly GeneralSoundSettingUserData soundSettingUserData;

#endregion

        public SimpleSettingPopupPresenter
        (
            IMessageService        messageService,
            GeneralSoundSettingUserData soundSettingUserData
        ) : base(messageService)
        {
            this.soundSettingUserData = soundSettingUserData;
        }

        protected override async UniTask OnViewInitAsync()
        {
            await base.OnViewInitAsync();
            this.View.SoundBtn.onClick.AddListener(this.OnClickSound);
            this.View.MusicBtn.onClick.AddListener(this.OnClickMusic);
            this.View.VibrateBtn.onClick.AddListener(this.OnClickVibrate);
            this.View.CloseBtn.onClick.AddListener(this.CloseView);
        }

        public override UniTask BindData()
        {
            this.View.SoundBtn.IsOn   = this.IsMusicOn;
            this.View.MusicBtn.IsOn   = this.IsSoundOn;
            this.View.VibrateBtn.IsOn = this.soundSettingUserData.IsVibrateOn;

            return UniTask.CompletedTask;
        }

        private bool IsMusicOn => Mathf.Abs(this.soundSettingUserData.MusicVolume - 1f) <= Mathf.Epsilon;

        private bool IsSoundOn => Mathf.Abs(this.soundSettingUserData.SoundVolume - 1f) <= Mathf.Epsilon;

        protected virtual void OnClickVibrate()
        {
            this.soundSettingUserData.IsVibrateOn = !this.soundSettingUserData.IsVibrateOn;
        }

        protected virtual void OnClickMusic()
        {
            var isOn = !this.IsMusicOn;
            this.soundSettingUserData.MusicVolume = isOn ? 1f : 0f;
        }

        protected virtual void OnClickSound()
        {
            var isOn = !this.IsSoundOn;
            this.soundSettingUserData.SoundVolume = isOn ? 1f : 0f;
        }
    }
}