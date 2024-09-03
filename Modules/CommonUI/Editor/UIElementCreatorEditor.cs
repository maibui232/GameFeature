namespace Modules.CommonUI.Editor
{
    using UnityEditor;
    using UnityEngine;

    public class UIElementCreatorEditor
    {
        [MenuItem("GameObject/UI/JoystickView")]
        public static void CreateJoystick()
        {
            var selectedObj = Selection.activeGameObject;
            var prefab      = AssetDatabase.LoadAssetAtPath<GameObject>("Packages/game.feature/Prefabs/UIElement/JoystickView.prefab");
            PrefabUtility.InstantiatePrefab(prefab, selectedObj.transform);
        }
    }
}