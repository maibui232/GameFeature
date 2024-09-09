namespace Modules.Quest.Runtime.Blueprint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameCore.Services.BlueprintFlow.BlueprintReader;
    using GameCore.Services.BlueprintFlow.BlueprintReader.Converter;
    using GameCore.Services.BlueprintFlow.BlueprintReader.Converter.TypeConversion;
    using GameExtensions.Runtime.Reflection;
    using Modules.Quest.Runtime.Interface;
    using Newtonsoft.Json;

    [BlueprintReader("QuestBlueprint")]
    public class QuestBlueprint : GenericBlueprintReaderByRow<string, QuestRecord>
    {
        public QuestBlueprint()
        {
            CsvHelper.RegisterTypeConverter(typeof(IQuestCondition), new TypeToJsonConverter<IQuestCondition>());
            CsvHelper.RegisterTypeConverter(typeof(IQuestReward), new TypeToJsonConverter<IQuestReward>());
        }
    }

    public class TypeToJsonConverter<T> : DefaultTypeConverter
    {
        private const    string                   Delimiter = "|";
        private readonly Dictionary<string, Type> nameToType;

        public TypeToJsonConverter()
        {
            this.nameToType = AppDomain.CurrentDomain.GetAllTypeFromDerived<T>()
               .ToDictionary(type => type.Name, type => type);
        }

        public override object ConvertFromString(string text, Type typeInfo)
        {
            var splits  = text.Split(Delimiter);
            var dataTxt = splits[1];
            var type    = this.nameToType[splits[0]];

            return JsonConvert.DeserializeObject(dataTxt, type);
        }
    }

    [CsvHeaderKey("Id")]
    public class QuestRecord
    {
        public string                Id          { get; private set; }
        public string                Name        { get; private set; }
        public string                Description { get; private set; }
        public List<IQuestCondition> Conditions  { get; private set; }
        public List<IQuestReward>    Rewards     { get; private set; }
    }
}