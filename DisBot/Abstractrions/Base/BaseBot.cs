using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DisBot.Abstractrions.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DisBot.Abstractrions.Base
{
    abstract class BaseBot : IBot
    {
        protected IEnumerable<IBotFunction> _botFunctions;
        protected readonly string _token;
        protected DiscordSocketClient? _client;
        public BaseBot(string token, IEnumerable<IBotFunction> botFunctions)
        {
            _botFunctions = botFunctions;
            _token = token;
        }
        public virtual void Build()
        {
            var disConfig = new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(disConfig);
        }
        public async Task Start()
        {
            if (_client is null) { throw new NullReferenceException(); }

            foreach (var botFunction in _botFunctions)
            {
                botFunction.Subscribe(ref _client);
            }

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
        }
        public async Task Stop()
        {
            if (_client is null) { throw new NullReferenceException(); }

            foreach (var botFunction in _botFunctions)
            {
                botFunction.Unsubscribe(ref _client);
            }

            await _client.StopAsync();
        }
    }
}
