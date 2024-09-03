namespace Modules.ItemDatabase.Runtime.UserData
{
    using System.Collections.Generic;
    using GameCore.Services.UserData.Interface;
    using Sirenix.Serialization;

    public class CurrencyUserData : IUserData
    {
        [OdinSerialize] public Dictionary<string, CurrencyData> IdToCurrencyData = new();

        public void Init()
        {
        }
    }

    public class CurrencyData
    {
        public CurrencyData(string currencyId, int value, int usedValue)
        {
            this.CurrencyId = currencyId;
            this.Value      = value;
            this.UsedValue  = usedValue;
        }

        public string CurrencyId { get; set; }
        public int    Value      { get; set; }
        public int    UsedValue  { get; set; }
    }
}