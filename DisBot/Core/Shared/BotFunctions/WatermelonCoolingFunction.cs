using DisBot.Abstractrions.Interfaces;
using Discord.WebSocket;
using Discord;
using DisBot.Core.Shared.Services;

namespace DisBot.Core.Shared.BotFunctions
{
    class WatermelonCoolingFunction : IBotFunction
    {
        private CommandStorage _storage = new CommandStorage();
        private bool IsWatermelonHeater = false;

        public WatermelonCoolingFunction(CommandStorage storage)
        {
            _storage = storage;
        }
        public void Subscribe(ref DiscordSocketClient _client)
        {
            _client.MessageReceived += SetEmoteToMelon;
            RegisterCommands();
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
        private void RegisterCommands()
        {
            var globalCommand = new SlashCommandBuilder();

            globalCommand.Name = "change_state";
            globalCommand.Description = "Меняет охлаждение на согревание и обратно";
            _storage.RegisterCommand(globalCommand, ChangeState);
        }
        private async Task ChangeState(SocketSlashCommand command)
        {
            IsWatermelonHeater = !IsWatermelonHeater;
            if (IsWatermelonHeater)
            {
                await command.RespondAsync("Теперь арбузы греются");
            }
            else
            {
                await command.RespondAsync("Теперь арбузы охлаждаются");
            }
        }
        private async Task SetEmoteToMelon(SocketMessage msg)
        {
            if (msg.Content == "🍉") 
            {
                if (IsWatermelonHeater)
                {
                    await msg.AddReactionAsync(new Emoji("🔥"));
                }
                else
                {
                    await msg.AddReactionAsync(new Emoji("❄️"));
                }
            }
        }
    }
}
