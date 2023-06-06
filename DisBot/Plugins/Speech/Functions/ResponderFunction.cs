using Discord.WebSocket;
using DisBot.Abstractrions.Interfaces;
using DisBot.Plugins.Speech.Common;

namespace DisBot.Plugins.Speech.Functions
{
    public class ResponderFunction : IBotFunction
    {
        public void Subscribe(ref DiscordSocketClient _client)
        {
            _client.MessageReceived += ReplyOnMessageRecieved;
        }
        public void Unsubscribe(ref DiscordSocketClient _client)  //may not work after bot has started 
        {
            try
            {
                _client.MessageReceived -= ReplyOnMessageRecieved;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during unsubscribe {e.Message}"); // TODO: remove this ugly stuff
            }
        }
        public async Task ReplyOnMessageRecieved(SocketMessage msg)
        {
            var rnd = new Random();
            if (msg.Author.IsBot) { return; }

            if (Dictionaries.greatingPhrases.ContainsKey(msg.Author.Id) && msg.Content == "Приветствуй")
            {
                await msg.Channel.SendMessageAsync(Dictionaries.greatingPhrases[msg.Author.Id]);
            }
            else if(msg.Content == "Приветствуй")
            {
                await msg.Channel.SendMessageAsync("Сам приветствуй, пёс");
            }

            if(msg.Content == "Славик")
            {
                int value = rnd.Next(1, 5);
                await msg.Channel.SendMessageAsync(Dictionaries.itsMe[value]);
            }
            return;
        }
    }
}
