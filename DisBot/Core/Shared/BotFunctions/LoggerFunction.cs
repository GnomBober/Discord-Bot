using DisBot.Abstractrions.Interfaces;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisBot.Core.Shared.BotFunctions
{
    class LoggerFunction : IBotFunction
    {
        public void Subscribe(ref DiscordSocketClient _client)
        {
            _client.Log += Log;
        }
        public void Unsubscribe(ref DiscordSocketClient _client)  //may not work after bot has started 
        {
            try
            {
                _client.Log -= Log;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during unsubscribe {e.Message}"); // TODO: remove this ugly stuff
            }
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
