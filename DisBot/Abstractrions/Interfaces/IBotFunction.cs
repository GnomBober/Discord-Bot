using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisBot.Abstractrions.Interfaces
{
    interface IBotFunction //TODO: make jopa function :^)
    {
        public void Subscribe(ref DiscordSocketClient _client);
        public void Unsubscribe(ref DiscordSocketClient _client);
    }
}
