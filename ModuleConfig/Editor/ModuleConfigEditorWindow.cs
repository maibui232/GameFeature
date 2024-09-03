namespace ModuleConfig.Editor
{
    using System;
    using Sirenix.OdinInspector.Editor;
    using GameExtensions.Runtime.Reflection;
    using UnityEditor;
    using UnityEngine;
    using System.IO;
    using ModuleConfig.Runtime;

    public class ModuleConfigEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Game/Feature/Config")]
        public static void OpenWindow()
        {
            var window = GetWindow<ModuleConfigEditorWindow>();
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            const string folderPath = "Assets/Configs/Editor";

            var tree = new OdinMenuTree();
            foreach (var type in AppDomain.CurrentDomain.GetAllTypeFromDerived<BaseModuleConfig>())
            {
                var nameType = type.Name;
                this.ValidateConfig(folderPath, type.Name, type);

                var assetPath = $"{folderPath}/{type.Name}.asset";
                tree.AddAssetAtPath(nameType, assetPath, type);
            }

            return tree;
        }

        private void ValidateConfig(string folderPath, string assetName, Type configType)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }

            var assetPath = $"{folderPath}//{assetName}.asset";
            if (!AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath))
            {
                var asset = CreateInstance(configType);
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.Refresh();
            }
        }
    }
}