using DisBot.Abstractrions.Interfaces;
using DisBot.Core.Shared.Services;
using Discord.WebSocket;
using Discord;

namespace DisBot.Core.Shared.BotFunctions
{
    class JopaFunction : IBotFunction
    {
        private CommandStorage _storage;
        public JopaFunction(CommandStorage storage)
        {
            _storage = storage;
        }
        public void Subscribe(ref DiscordSocketClient _client)
        {
            RegisterCommands();
        }
        public void Unsubscribe(ref DiscordSocketClient _client)  //may not work after bot has started 
        {
        }
        private void RegisterCommands()
        {
            var globalCommand = new SlashCommandBuilder();

            globalCommand.Name = "jopa";
            globalCommand.Description = "Функция жопа :^)";

            _storage.RegisterCommand(globalCommand, ReplyWithJopa);
        }
        private async Task ReplyWithJopa(SocketSlashCommand command)
        {
            await command.RespondAsync(":^)");
        }
    }
}
