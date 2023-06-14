using DisBot.Plugins.ValueBot.Abstractions;
using DisBot.Plugins.ValueBot.Models;
using System.Security.Cryptography.X509Certificates;

namespace DisBot.Plugins.ValueBot.Services
{
    class StubValueStorage : IValueStorage
    {
        private static readonly StubValueStorage instance = new StubValueStorage(); 
        //https://metanit.com/sharp/patterns/2.3.php singleton from metanit :^) this thing emulates behavior of storage; dont use this patterns in future i beg u

        Dictionary <ulong, long> UserAccounts;
        private StubValueStorage()
        {
            UserAccounts = new Dictionary<ulong, long>();
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
        public async Task<bool> WithdrawValue(TransactionModel transaction) 
        {
            if (await GetValue(transaction.UserIdFrom) < transaction.Value)
                return false;

            UserAccounts[transaction.UserIdFrom] -= transaction.Value;
            return true;
        }
        public async Task<bool> TransferValue(TransactionModel transaction)
        {
            var withdrowResult = await WithdrawValue(transaction);

            if (!withdrowResult) return false;

            await AddValue(transaction);

            return true;
        }
        public Task<long> GetValue(ulong userId) 
        {
            if (!UserAccounts.ContainsKey(userId))
            {
                UserAccounts[userId] = 1000;
            }

            return Task.FromResult(UserAccounts[userId]);
        }
        public Task<IEnumerable<KeyValuePair<ulong, long>>> GetNetwothes(int skip, int take, Order order, bool desc = true)
        {
            IEnumerable<KeyValuePair<ulong, long>> tempResult = UserAccounts.AsEnumerable();

            if (desc)
            {

                if (order == Order.ById)
                    tempResult = tempResult.OrderByDescending(x => x.Key);
                else if (order == Order.ByBalance)
                    tempResult = tempResult.OrderByDescending(x => x.Value);
                else
                    throw new NotImplementedException();
            }
            else
            {
                if (order == Order.ById)
                    tempResult = tempResult.OrderBy(x => x.Key);
                else if (order == Order.ByBalance)
                    tempResult = tempResult.OrderBy(x => x.Value);
                else
                    throw new NotImplementedException();
            }
            
            return Task.FromResult(tempResult.Skip(skip).Take(take));
        }
    }
}
