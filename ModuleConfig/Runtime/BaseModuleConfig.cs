namespace ModuleConfig.Runtime
{
    using Sirenix.OdinInspector;
    using UnityEngine;
#if UNITY_EDITOR
    using GameExtensions.Editor;
#endif

    public abstract class BaseModuleConfig : ScriptableObject
    {
        [SerializeField, OnValueChanged("OnEnableChange")]
        private bool enable;

        public bool Enable => this.enable;

#if UNITY_EDITOR
        protected abstract string ScriptDefineSymbol { get; }

        private void OnEnableChange(bool isEnable)
        {
            if (isEnable)
            {
                ScriptDefineUtils.AddDefine(this.ScriptDefineSymbol);
            }
            else
            {
                ScriptDefineUtils.RemoveDefine(this.ScriptDefineSymbol);
            }
        }
#endif
    }
}