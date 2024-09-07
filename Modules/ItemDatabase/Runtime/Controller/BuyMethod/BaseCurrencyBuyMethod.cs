namespace Modules.ItemDatabase.Runtime.Controller.BuyMethod
{
    using System;
    using Modules.GameFeel.Runtime.VfxAttractor;
    using UnityEngine;

    public abstract class BaseCurrencyBuyMethod : IBuyMethod
    {
#region Injectt

        private readonly CurrencyDataController currencyDataController;
        private readonly IVfxAttractorService   attractorService;

#endregion

        protected BaseCurrencyBuyMethod
        (
            CurrencyDataController currencyDataController,
            IVfxAttractorService   attractorService
        )
        {
            this.currencyDataController = currencyDataController;
            this.attractorService       = attractorService;
        }

        public abstract string BuyMethodId { get; }

        public void Process(GameObject source, GameObject target, int price, Action<string> onCompleted, Action<string> onFailed)
        {
            this.currencyDataController.AddCurrency(this.BuyMethodId, price, OnCompleted, OnFailed);

            return;

            void OnCompleted(CurrencyCallbackData callbackData)
            {
                if (source != null && target != null)
                {
                    // Show Vfx currency
                    var record = this.currencyDataController.GetCurrencyRecord(this.BuyMethodId);
                    this.attractorService.SpawnAttractor(record.VfxAddressable, source.transform.position, target.transform);
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