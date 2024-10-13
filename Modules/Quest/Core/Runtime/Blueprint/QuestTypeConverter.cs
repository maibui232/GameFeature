namespace Modules.Quest.Core.Runtime.Blueprint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using Modules.Quest.Core.Runtime.Model.Condition;
    using Modules.Quest.Core.Runtime.Model.Progress;
    using Modules.Quest.Core.Runtime.Model.Reward;
    using Newtonsoft.Json;
    using Services.Blueprint.Converter.TypeConverter;

    public abstract class TypeToJsonConverter<T> : DefaultTypeConverter
    {
        private const    char                     Delimiter = '?';
        private readonly Dictionary<string, Type> nameToType;

        public override Type TargetType => typeof(T);

        protected TypeToJsonConverter()
        {
            this.nameToType = AppDomain.CurrentDomain.GetAllTypeFromDerived<T>()
               .ToDictionary(type => type.Name, type => type);
        }

        public override object DeserializeFromCsv(Type type, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            var splits     = value.Split(Delimiter);
            var dataTxt    = splits[1];
            var targetType = this.nameToType[splits[0]];

            return JsonConvert.DeserializeObject(dataTxt, targetType);
        }

        public override string SerializeToCsv(Type type, object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var typeName = type.Name;
            var jsonData = JsonConvert.SerializeObject(value);

            return $"{typeName}{Delimiter}{jsonData}";
        }
    }

    public class QuestConditionConverter : TypeToJsonConverter<IQuestCondition>
    {
    }

    public class QuestRewardConverter : TypeToJsonConverter<IQuestReward>
    {
    }

    public class QuestProgressConverter : TypeToJsonConverter<IQuestProgress>
    {
    }
}