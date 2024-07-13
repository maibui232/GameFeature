namespace Feature.Modules.CommonUI.Runtime.Element
{
    using UnityEngine;
    using UnityEngine.UI;

    public class OnOffButton : Button
    {
        [SerializeField] private GameObject onStatusObj;
        [SerializeField] private GameObject offStatusObj;

        private bool isOn;

        public bool IsOn
        {
            get => this.isOn;
            set
            {
                this.isOn = value;
                this.onStatusObj.SetActive(value);
                this.offStatusObj.SetActive(!value);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            this.onClick.AddListener(this.OnClickButton);
        }

        private void OnClickButton()
        {
            this.IsOn = !this.IsOn;
        }
    }
}