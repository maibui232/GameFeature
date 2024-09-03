namespace Modules.ItemDatabase.Runtime.Controller.BuyMethod
{
    using System;
    using GameCore.Services.ObjectPool;
    using UnityEngine;

    public class CurrencyBuyMethod : IBuyMethod
    {
#region Inject

        private readonly CurrencyDataController currencyDataController;
        private readonly IObjectPoolService     objectPoolService;

#endregion

        public CurrencyBuyMethod
        (
            CurrencyDataController currencyDataController,
            IObjectPoolService     objectPoolService
        )
        {
            this.currencyDataController = currencyDataController;
            this.objectPoolService      = objectPoolService;
        }

        public string BuyMethodId { get; internal set; }

        public void Process(GameObject source, int price, Action<string> onCompleted, Action<string> onFailed)
        {
            this.currencyDataController.AddCurrency(this.BuyMethodId, price, OnCompleted, OnFailed);

            return;

            void OnCompleted(CurrencyCallbackData callbackData)
            {
                if (source != null)
                {
                    // Show Vfx currency
                    // var record = this.currencyDataController.GetCurrencyRecord(this.BuyMethodId);
                    // this.objectPoolService.Spawn(record.VfxAddressable);
                }

                onCompleted?.Invoke(callbackData.Msg);
            }

            void OnFailed(CurrencyCallbackData callbackData)
            {
                onFailed?.Invoke(callbackData.Msg);
            }
        }
    }
}