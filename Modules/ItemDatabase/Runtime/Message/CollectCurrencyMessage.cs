namespace Modules.ItemDatabase.Runtime.Message
{
    public struct CollectCurrencyMessage
    {
        public string CurrencyId { get; }
        public int    Amount     { get; }

        public CollectCurrencyMessage(string currencyId, int amount)
        {
            this.CurrencyId = currencyId;
            this.Amount     = amount;
        }
    }
}