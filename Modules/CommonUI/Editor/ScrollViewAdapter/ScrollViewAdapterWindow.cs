namespace Modules.CommonUI.Editor.ScrollViewAdapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using Plugins.EnhancedScroller_v2.Plugins;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using ScrollDirectionEnum = Plugins.EnhancedScroller_v2.Plugins.EnhancedScroller.ScrollDirectionEnum;
    using TextElement = UnityEngine.UIElements.TextElement;

    public class ScrollViewAdapterWindow : EditorWindow
    {
        [MenuItem("Window/Features/CommonUI/ScrollViewAdapter")]
        [MenuItem("GameObject/UI/ScrollerViewAdapter")]
        public static void OpenWindow()
        {
            var size   = new Vector2(350, 300);
            var window = GetWindow<ScrollViewAdapterWindow>();
            window.maxSize = size;
            window.minSize = size;
            window.Show();
        }

        private Type                selectedAdapterType;
        private StyleSheet          sheet;
        private IEnumerable<Type>   adapterTypes;
        private ScrollDirectionEnum direction;
        private VisualElement       setupPanel;
        private VisualElement       completedPanel;

        private GameObject GetParent()
        {
            if (Selection.activeGameObject != null)
            {
                return Selection.activeGameObject;
            }

            var firstCanvas = FindFirstObjectByType<Canvas>();

            if (firstCanvas != null) return firstCanvas.gameObject;

            EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
            var instance = FindFirstObjectByType<Canvas>();

            return instance.gameObject;
        }

        private void CreateGUI()
        {
            this.CreateSetupPanel();
            this.CreateCompletedPanel();
            this.ShowCompleted(false);
        }

        private void CreateSetupPanel()
        {
            this.setupPanel = new VisualElement();
            this.rootVisualElement.Add(this.setupPanel);

            var scrollDirectionEnumField = new EnumField("Direction", ScrollDirectionEnum.Horizontal);
            scrollDirectionEnumField.RegisterValueChangedCallback(this.OnChangeScrollerDirection);
            this.setupPanel.Add(scrollDirectionEnumField);

            this.adapterTypes = AppDomain.CurrentDomain.GetAllTypeFromDerived<EnhancedScroller>();
            var options = this.adapterTypes.Where(type => !type.Name.Equals(nameof(EnhancedScroller)))
               .Select(type => type.Name)
               .ToList();
            var canSelectAdapter = options.Count != 0;
            if (canSelectAdapter)
            {
                var adapterDropdown = new DropdownField("Adapter", options, options.First(), this.OnSelectAdapter);
                this.setupPanel.Add(adapterDropdown);
                var instantiateBtn = new Button(this.OnClickInstantiate)
                                     {
                                         text = "Instantiate"
                                     };
                this.setupPanel.Add(instantiateBtn);
            }
            else
            {
                this.setupPanel.Add(new TextElement
                                    {
                                        text = "Need to implement Adapter"
                                    });
            }
        }

        private void CreateCompletedPanel()
        {
            this.completedPanel = new VisualElement();
            var text = new TextElement
                       {
                           text = "Completed instantiate ScrollViewAdapter!!!",
                           style =
                           {
                               fontSize  = 28,
                               alignSelf = new StyleEnum<Align>(Align.Center)
                           }
                       };
            this.completedPanel.Add(text);
            this.rootVisualElement.Add(this.completedPanel);
        }

        private void ShowCompleted(bool isCompleted)
        {
            this.setupPanel.visible     = !isCompleted;
            this.completedPanel.visible = isCompleted;
        }

        private string OnSelectAdapter(string arg)
        {
            this.selectedAdapterType = this.adapterTypes.FirstOrDefault(t => t.Name.Equals(arg));

            return arg;
        }

        private void OnClickInstantiate()
        {
            if (this.selectedAdapterType == null) return;
            var prefab   = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/game.feature/Modules/CommonUI/Prefabs/UIElement/ScrollViewAdapter.prefab");
            var parent   = this.GetParent();
            var instance = Instantiate(prefab, parent.transform);
            instance.name = this.selectedAdapterType.Name;
            instance.AddComponent(this.selectedAdapterType);
            if (instance.TryGetComponent<EnhancedScroller>(out var scroller))
            {
                scroller.scrollDirection   = this.direction;
                Selection.activeGameObject = instance;
                this.ShowCompleted(true);
            }
        }

        private void OnChangeScrollerDirection(ChangeEvent<Enum> evt)
        {
            this.direction = (ScrollDirectionEnum)evt.newValue;
        }
    }
}