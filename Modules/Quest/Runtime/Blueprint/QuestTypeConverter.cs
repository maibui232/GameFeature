namespace Modules.Quest.Runtime.Blueprint
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GameExtensions.Runtime.Reflection;
    using Modules.Quest.Runtime.Interface;
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
            var splits     = value.Split(Delimiter);
            var dataTxt    = splits[1];
            var targetType = this.nameToType[splits[0]];

            return JsonConvert.DeserializeObject(dataTxt, targetType);
        }

        public override string SerializeToCsv(Type type, object value)
        {
            var typeName = typeof(T).Name;
            var jsonData = JsonConvert.SerializeObject(value);

            return $"{typeName}{Delimiter}{jsonData}";
        }
    }

    public class ConditionTypeConverter : TypeToJsonConverter<IQuestCondition>
    {
    }

    public class RewardTypeConverter : TypeToJsonConverter<IQuestReward>
    {
    }
}