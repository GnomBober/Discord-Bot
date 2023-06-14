using DisBot.Plugins.ValueBot.Models;

namespace DisBot.Plugins.ValueBot.Abstractions
{
    interface IValueStorage
    {
        /// <summary>
        /// Add money to user (new should be created on empty) (should return false on UserIdTo empty)
        /// </summary>
        /// <returns></returns>
        public Task AddValue(TransactionModel transaction);
        /// <summary>
        /// Withdraw money from user account (should return false on UserIdFrom empty)
        /// </summary>
        /// <returns>true if successful</returns>
        public Task<bool> WithdrawValue(TransactionModel transaction);
        /// <summary>
        /// Withdraw money from one and add to other (should return false on some of user ids are empty)
        /// </summary>
        /// <returns>true if successful</returns>
        public Task<bool> TransferValue(TransactionModel transaction);
        /// <summary>
        /// Get the amount of money for this dude
        /// </summary>
        /// <param name="userId">Discord snowflake</param>
        /// <returns>The amount</returns>
        public Task<long> GetValue(ulong userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"> how many skipped </param>
        /// <param name="take"> how many requested </param>
        /// <param name="order">order of elements</param>
        /// <param name="desc">is descending order</param>
        /// <returns></returns>
        public Task<IEnumerable<KeyValuePair<ulong, long>>> GetNetwothes(int skip, int take, Order order, bool desc = true);
    }
}
