namespace Modules.BattlePass.Editor
{
    using ModuleConfig.Editor.Interfaces;
    using System;
    using Feature.Modules.BattlePass.Runtime.Config;

    public class BattlePassEditorConfig : IEditorModuleConfig
    {
        public Type ModuleConfigType => typeof(BattlePassConfig);
    }
}