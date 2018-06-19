using System;

namespace ChangellyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Changelly.Changelly changelly = new Changelly.Changelly("acda5d2cadb14a14b1d2027dd89b2e4d", "5ee56c4229105c847f6a8cdc8e99aa6a7c6bbf1c88837137dd0de5cc4fb7721e", "https://api.changelly.com");
            //changelly.GetCurrenciesFull();
            //changelly.GetCurrencies();
           var res = changelly.GetExchangeAmount("btc", "usd", 1);
            //changelly.GetMinAmount("eth", "btc");
            // changelly.CreateTransaction("btc", "eth", "0xe7E5c18aA9878c8a55e610FF39f3fa48d48DD15", 0.2);
            //changelly.GetStatus("bb231c589f4");
            //changelly.GetTransactions();
        }
    }
}
