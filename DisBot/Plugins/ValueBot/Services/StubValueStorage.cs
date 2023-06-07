using DisBot.Plugins.ValueBot.Abstractions;
using DisBot.Plugins.ValueBot.Models;

namespace DisBot.Plugins.ValueBot.Services
{
    class StubValueStorage : IValueStorage
    {
        private static readonly StubValueStorage instance = new StubValueStorage(); 
        //https://metanit.com/sharp/patterns/2.3.php singleton from metanit :^) this thing emulates behavior of storage; dont use this patterns in future i beg u

        Dictionary <ulong, int> UserAccounts;
        private StubValueStorage()
        {
            UserAccounts = new Dictionary<ulong, int>();
        }

        public static StubValueStorage GetInstance()
        {
            return instance;
        }
        public Task AddValue(TransactionModel transaction)
        {
            if (UserAccounts.ContainsKey(transaction.UserIdTo))
            {
                UserAccounts[transaction.UserIdTo] += transaction.Value;
            }
            else
            {
                UserAccounts[transaction.UserIdTo] = transaction.Value;
            }
            return Task.CompletedTask;
        }
        public Task<bool> WithdrawValue(TransactionModel transaction) 
        {
            if(!UserAccounts.ContainsKey(transaction.UserIdFrom))
                return Task.FromResult(false);

            if (UserAccounts[transaction.UserIdFrom] < transaction.Value)
                return Task.FromResult(false);

            UserAccounts[transaction.UserIdFrom] -= transaction.Value;
            return Task.FromResult(true);
        }
        public async Task<bool> TransferValue(TransactionModel transaction)
        {
            var withdrowResult = await WithdrawValue(transaction);

            if (!withdrowResult) return false;

            await AddValue(transaction);

            return true;
        }
        public Task<int> GetValue(ulong userId) 
        {
            return Task.FromResult(UserAccounts[userId]);
        }
    }
}
