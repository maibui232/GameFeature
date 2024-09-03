namespace ModuleConfig.Runtime
{
    using Sirenix.OdinInspector;
    using UnityEngine;
#if UNITY_EDITOR
    using System.IO;
    using GameExtensions.Editor;
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
                this.CreateBlueprint();
            }
            else
            {
                ScriptDefineUtils.RemoveDefine(this.ScriptDefineSymbol);
            }
        }

        protected virtual void CreateBlueprint()
        {
        }

        protected void CreateCsvFile(string fileName, string fileContent)
        {
            const string blueprintRootFolder = "Assets/Resources/BlueprintData/";
            var          blueprintPath       = $"{blueprintRootFolder}{fileName}.csv";
            var          asset               = AssetDatabase.LoadAssetAtPath<TextAsset>(blueprintPath);

            if (asset != null) return;
            using TextWriter writer = new StreamWriter(blueprintPath);
            writer.WriteLine(fileContent);
            writer.Close();
            AssetDatabase.Refresh();
        }
#endif
    }
}