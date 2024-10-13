namespace Modules.PlayerData.Runtime.Config
{
    using System;
    using Cysharp.Threading.Tasks;
    using ModuleConfig.Runtime;
    using Modules.PlayerData.Runtime.Blueprint;
    using Services.Blueprint.ReaderFlow;
    using Sirenix.OdinInspector;

    public class PlayerConfig : BaseModuleConfig
    {
        protected override string ScriptDefineSymbol => "PLAYER_DATA";

#region Editor

#if UNITY_EDITOR

        [ShowInInspector] private PlayerLevelBlueprint playerLevelBlueprint;

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private async void LoadBlueprint()
        {
            try
            {
                this.playerLevelBlueprint = await EditorBlueprintReader.OpenReadBlueprint<PlayerLevelBlueprint>();
            }
            catch (Exception e)
            {
                this.playerLevelBlueprint = new();
                this.SaveBlueprint();
            }
        }

        [PropertyOrder(0)]
        [ButtonGroup]
        [Button(ButtonSizes.Gigantic)]
        private void SaveBlueprint()
        {
            EditorBlueprintReader.SaveBlueprint(this.playerLevelBlueprint).Forget();
        }

#endif

#endregion
    }
}