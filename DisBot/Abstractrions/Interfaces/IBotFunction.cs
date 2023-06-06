using Discord.WebSocket;

namespace DisBot.Abstractrions.Interfaces
{
    interface IBotFunction //TODO: make jopa function :^)
    {
        public void Subscribe(ref DiscordSocketClient _client);
        public void Unsubscribe(ref DiscordSocketClient _client);
    }
}
