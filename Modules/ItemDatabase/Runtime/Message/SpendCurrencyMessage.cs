namespace Modules.ItemDatabase.Runtime.Message
{
    public struct SpendCurrencyMessage
    {
        public string CurrencyId { get; }
        public int    Amount     { get; }

        public SpendCurrencyMessage(string currencyId, int amount)
        {
            this.CurrencyId = currencyId;
            this.Amount     = amount;
        }
    }
}