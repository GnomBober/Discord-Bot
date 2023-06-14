﻿namespace DisBot.Plugins.ValueBot.Models
{
    class TransactionModel
    {
        public ulong UserIdFrom { get; set; }
        public ulong UserIdTo { get; set; }
        public long Value { get; set; }
        public DateTime Created { get; } = DateTime.Now;
    }
}
