namespace Modules.CommonUI.Runtime.Element.Selector
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Dropdown))]
    public class DropDownSelector : BaseSelector
    {
        [HideInInspector, SerializeField] private TMP_Dropdown dropdown;

        private void OnValidate()
        {
            this.ValidateField();
        }

        private void ValidateField()
        {
            this.dropdown ??= this.GetComponent<TMP_Dropdown>();
        }

        private void Awake()
        {
            this.ValidateField();
            this.dropdown.onValueChanged.AddListener(this.OnValueChange);
        }

        private void OnValueChange(int index)
        {
            this.SelectedIndex = index;
            this.OnSelected?.Invoke(this.dropdown.options[index]);
        }

        public override void Initialize(List<TMP_Dropdown.OptionData> listOptionData)
        {
            this.dropdown.options = listOptionData;
        }
    }
}