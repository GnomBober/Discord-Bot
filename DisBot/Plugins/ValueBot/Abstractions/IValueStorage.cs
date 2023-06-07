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
        public Task<int> GetValue(ulong userId);
    }
}
