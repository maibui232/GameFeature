namespace Modules.ItemDatabase.Runtime.Controller.BuyMethod
{
    using System;
    using UnityEngine;

    public interface IBuyMethod
    {
        string BuyMethodId { get; }
        void   Process(GameObject source, GameObject target, int price, Action<string> onCompleted, Action<string> onFailed);
    }
}