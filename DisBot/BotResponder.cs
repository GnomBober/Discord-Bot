using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DisBot.Speech
{
    public class BotResponder
    {
        
        public async Task Greatings(SocketMessage msg)
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
