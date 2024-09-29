namespace ModuleConfig.Runtime
{
    using System.Reflection;
    using Sirenix.OdinInspector;
    using UnityEngine;
#if UNITY_EDITOR
    using System;
    using System.IO;
    using GameExtensions.Editor;
    using Services.Blueprint.Attribute;
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

        protected void CreateCsvFile(Type blueprintType)
        {
            var header = this.HeaderBlueprintFile(blueprintType);

            if (string.IsNullOrEmpty(header)) return;

            var filePath = this.BlueprintFilePath(blueprintType);
            var asset    = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);

            if (asset != null) return;

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(header);
                writer.Close();
            }

            AssetDatabase.Refresh();
        }

        protected void AppendTextToBlueprintFile(Type blueprintType, string content)
        {
            var filePath = this.BlueprintFilePath(blueprintType);
            if (AssetDatabase.LoadAssetAtPath<TextAsset>(filePath) == null)
            {
                this.CreateCsvFile(blueprintType);
            }

            using (var writer = System.IO.File.AppendText(filePath))
            {
                writer.WriteLine(content);
                writer.Close();
            }

            AssetDatabase.Refresh();
        }

        protected string BlueprintFilePath(Type blueprintType)
        {
            const string blueprintRootFolder = "Assets/Resources/BlueprintData/";
            var          fileName            = blueprintType.GetCustomAttribute<CsvReaderAttribute>().Path;

            return $"{blueprintRootFolder}{fileName}.csv";
        }

        protected string HeaderBlueprintFile(Type blueprintType)
        {
            if (blueprintType.BaseType == null) return string.Empty;
            var genericArguments = blueprintType.BaseType.GetGenericArguments();

            if (genericArguments.Length == 0) return string.Empty;

            var recordType = blueprintType.BaseType.GetGenericArguments()[^1];
            var header     = this.GetRecursiveHeaderBlueprint(recordType);
            header = header.TrimEnd(',');

            return header;
        }

        protected string GetRecursiveHeaderBlueprint(Type recordType)
        {
            var header = string.Empty;
            foreach (var propInfo in recordType.GetProperties())
            {
                if (typeof(IBlueprintCollection).IsAssignableFrom(propInfo.PropertyType))
                {
                    if (propInfo.PropertyType.BaseType == null) continue;
                    var genericArguments = propInfo.PropertyType.BaseType.GetGenericArguments();

                    if (genericArguments.Length == 0) continue;
                    header += this.GetRecursiveHeaderBlueprint(genericArguments[^1]);
                }
                else
                {
                    header += $"{propInfo.Name},";
                }
            }

            return header;
        }
#endif
    }
}