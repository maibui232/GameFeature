namespace ModuleConfig.Editor
{
    using System;
    using ModuleConfig.Editor.Interfaces;
    using Sirenix.OdinInspector.Editor;
    using GameExtensions.Runtime.Reflection;
    using UnityEditor;
    using UnityEngine;
    using System.IO;

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
            foreach (var editorModuleConfig in AppDomain.CurrentDomain.GetAllInstanceFromDerived<IEditorModuleConfig>())
            {
                var menuItem   = editorModuleConfig.GetType().Name;
                var configType = editorModuleConfig.ModuleConfigType;
                this.ValidateConfig(folderPath, configType.Name, configType);

                var assetPath = $"{folderPath}/{configType.Name}.asset";
                tree.AddAssetAtPath(menuItem, assetPath, configType);
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