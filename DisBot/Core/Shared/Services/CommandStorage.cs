using DisBot.Abstractrions.Interfaces;
using Discord;
using Discord.WebSocket;

namespace DisBot.Core.Shared.Services
{
    class CommandStorage : IBotFunction
    {
        public Dictionary<string, Func<SocketSlashCommand, Task>> CommandDictionary { get; private set; } = new Dictionary<string, Func<SocketSlashCommand, Task>>();
        public List<SlashCommandBuilder> CommandBuilders { get; private set; } = new List<SlashCommandBuilder>();
        public bool IsSubscribed { get; private set; } = false;
        private DiscordSocketClient _client;
        public void Subscribe(ref DiscordSocketClient client)
        {
            _client = client;
            client.SlashCommandExecuted += HandleCommand;
            client.Ready += Ready;
        }
        public void Unsubscribe(ref DiscordSocketClient client)  //may not work after bot has started 
        {
            try
            {
                _client = null;
                client.SlashCommandExecuted -= HandleCommand;
                IsSubscribed = false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during unsubscribe {e.Message}"); // TODO: remove this ugly stuff
            }
        }
        private async Task Ready()
        {
            IsSubscribed = true;
            var commandArray = buildCommands().ToArray();
            await _client.BulkOverwriteGlobalApplicationCommandsAsync(commandArray);
        }
        private IEnumerable<ApplicationCommandProperties> buildCommands()
        {
            foreach(var command in CommandBuilders)
            {
                yield return command.Build();
            }
        }
        private async Task HandleCommand(SocketSlashCommand command)
        {
            if (CommandDictionary.ContainsKey(command.CommandName))
            {
                await CommandDictionary[command.CommandName].Invoke(command);
            }
        }
        public void RegisterCommand(SlashCommandBuilder builder, Func<SocketSlashCommand, Task> action) 
        {
            if (IsSubscribed)
                throw new AccessViolationException("Command registering not supported after subscription!");

            CommandBuilders.Add(builder);
            CommandDictionary.Add(builder.Name, action);
        }
    }
}
