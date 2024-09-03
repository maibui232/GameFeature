namespace Modules.CommonUI.Runtime.Element.Selector
{
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public interface ISelector
    {
        int  SelectedIndex { get; }
        void Initialize(List<TMP_Dropdown.OptionData> listOptionData);
    }

    public abstract class BaseSelector : MonoBehaviour, ISelector
    {
        public int SelectedIndex { get; protected set; }

        public abstract void Initialize(List<TMP_Dropdown.OptionData> listOptionData);

        public Action<TMP_Dropdown.OptionData> OnSelected;
    }
}