namespace ModuleConfig.Runtime
{
    using System.Reflection;
    using Cysharp.Threading.Tasks;
    using Sirenix.OdinInspector;
    using UnityEngine;
#if UNITY_EDITOR
    using System;
    using System.IO;
    using GameExtensions.Editor;
    using Services.Blueprint.Attribute;
    using Services.Blueprint.ReaderFlow;
    using Services.Blueprint.ReaderFlow.GenericReader;
    using UnityEditor;
#endif

    public abstract class BaseModuleConfig : ScriptableObject
    {
        [SerializeField, OnValueChanged("OnEnableChange")]
        private bool enable;

        public bool Enable => this.enable;

#if UNITY_EDITOR
        protected abstract string ScriptDefineSymbol { get; }

        protected virtual void OnEnableChange(bool isEnable)
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

        protected async UniTask<T> LoadBlueprint<T>() where T : IGenericBlueprintReader, new()
        {
            T blueprint;
            try
            {
                blueprint = await EditorBlueprintReader.OpenReadBlueprint<T>();
            }
            catch (Exception)
            {
                blueprint = new T();
                await EditorBlueprintReader.SaveBlueprint(blueprint);
            }

            return blueprint;
        }
#endif
    }
}