namespace Modules.ItemDatabase.Runtime.Controller
{
    using System;
    using Modules.ItemDatabase.Runtime.Blueprint;
    using Modules.ItemDatabase.Runtime.UserData;
    using UnityEngine;

    public interface ICurrencyDataController
    {
        CurrencyData   GetCurrencyData(string   id);
        CurrencyRecord GetCurrencyRecord(string id);

        void AddCurrency
        (
            string                       id,
            int                          additionValue,
            Action<CurrencyCallbackData> onCompleted = null,
            Action<CurrencyCallbackData> onFailed    = null
        );
    }

    public class CurrencyDataController : ICurrencyDataController
    {
#region Inject

        private readonly CurrencyBlueprint currencyBlueprint;
        private readonly CurrencyUserData  currencyUserData;

#endregion

        public CurrencyDataController
        (
            CurrencyBlueprint currencyBlueprint,
            CurrencyUserData  currencyUserData
        )
        {
            this.currencyBlueprint = currencyBlueprint;
            this.currencyUserData  = currencyUserData;
        }

        public CurrencyData GetCurrencyData(string id)
        {
            if (this.currencyUserData.IdToCurrencyData.TryGetValue(id, out var data))
            {
                return data;
            }

            var record = this.GetCurrencyRecord(id);
            data = new CurrencyData(id, record.Min, 0);
            this.currencyUserData.IdToCurrencyData.Add(id, data);

            return data;
        }

        public CurrencyRecord GetCurrencyRecord(string id)
        {
            return this.currencyBlueprint.GetRecordByKey(id);
        }

        public void AddCurrency
        (
            string                       id,
            int                          additionValue,
            Action<CurrencyCallbackData> onCompleted = null,
            Action<CurrencyCallbackData> onFailed    = null
        )
        {
            var currencyData = this.GetCurrencyData(id);

            if (additionValue < 0)
            {
                if (currencyData.Value < additionValue)
                {
                    onFailed?.Invoke(new CurrencyCallbackData(id, "Doesn't have enough addition value to subtract currency"));

                    return;
                }

                currencyData.UsedValue += Mathf.Abs(additionValue);
            }

            currencyData.Value += additionValue;

            var record = this.GetCurrencyRecord(id);
            if (currencyData.Value > record.Max)
            {
                currencyData.Value = record.Max;
            }

            onCompleted?.Invoke(new CurrencyCallbackData(id, $"Currency {id} value: {currencyData.Value}"));
        }
    }

    public struct CurrencyCallbackData
    {
        public CurrencyCallbackData(string currencyId, string msg)
        {
            this.CurrencyId = currencyId;
            this.Msg        = msg;
        }

        public string CurrencyId { get; }
        public string Msg        { get; }
    }
}