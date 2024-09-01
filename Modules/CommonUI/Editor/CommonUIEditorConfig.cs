namespace CommonUI.Modules.CommonUI.Editor
{
    using System;
    using Feature.Modules.CommonUI.Runtime.Config;
    using ModuleConfig.Editor.Interfaces;

    public class CommonUIEditorConfig : IEditorModuleConfig
    {
        public Type ModuleConfigType => typeof(CommonUIConfig);
    }
}