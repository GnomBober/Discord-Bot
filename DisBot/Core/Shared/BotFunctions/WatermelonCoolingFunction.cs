using DisBot.Abstractrions.Interfaces;
using Discord.WebSocket;
using Discord;
using DisBot.Plugins.Speech.Common;

namespace DisBot.Core.Shared.BotFunctions
{
    class WatermelonCoolingFunction : IBotFunction
    {
        public void Subscribe(ref DiscordSocketClient _client)
        {
            _client.MessageReceived += SetEmoteToMelon;
        }
        public void Unsubscribe(ref DiscordSocketClient _client)  //may not work after bot has started 
        {
            try
            {
                _client.MessageReceived += SetEmoteToMelon;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during unsubscribe {e.Message}"); // TODO: remove this ugly stuff
            }
        }
        public async Task SetEmoteToMelon(SocketMessage msg)
        {
            if (msg.Content == "🍉") 
            {
                await msg.AddReactionAsync(new Emoji("❄️"));
            }
        }
    }
}
