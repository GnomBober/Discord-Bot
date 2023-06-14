using DisBot.Abstractrions.Interfaces;
using DisBot.Core.Shared.Services;
using DisBot.Plugins.ValueBot.Abstractions;
using Discord.WebSocket;
using Discord;
using System.Text;

namespace DisBot.Plugins.ValueBot.Functions
{
    class ValueCommandsFunction : IBotFunction
    {
        private CommandStorage _commandStorage;
        private IValueStorage _valueStorage;
        private DiscordSocketClient _socketClient;
        public Dictionary<string, Func<SocketSlashCommand, Task>> _subCommandDictionary { get; private set; } = new Dictionary<string, Func<SocketSlashCommand, Task>>();
        public ValueCommandsFunction(CommandStorage storage, IValueStorage valueStorage)
        {
            _subCommandDictionary.Add("get", GetValue);
            _subCommandDictionary.Add("pay", PayTo);
            _subCommandDictionary.Add("top", GetTop);

            _commandStorage = storage;
            _valueStorage = valueStorage;
        }
        public void Subscribe(ref DiscordSocketClient client)
        {
            _socketClient = client;
            RegisterCommands();
        }
        public void Unsubscribe(ref DiscordSocketClient client)  //may not work after bot has started 
        {
        }
        private void RegisterCommands()
        {
            var globalCommand = new SlashCommandBuilder()
                .WithName("balance")
                .WithDescription("Операции с деньгами")
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("get")
                    .WithDescription("Посмотреть скока денях")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                 )
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("top")
                    .WithDescription("топ 5 богатых")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                )
                .AddOption(new SlashCommandOptionBuilder()
                    .WithName("pay")
                    .WithDescription("Посмотреть скока денях")
                    .WithType(ApplicationCommandOptionType.SubCommand)
                    .AddOption("amount", ApplicationCommandOptionType.Integer, "скока хош заплатить", isRequired: true)
                    .AddOption("user", ApplicationCommandOptionType.User, "кому хочешь", isRequired: true)
                );

            _commandStorage.RegisterCommand(globalCommand, HandleSubCommands);
        }

        private async Task HandleSubCommands(SocketSlashCommand command)
        {
            var subcommand = command.Data.Options.First().Name;

            if (subcommand == null)
            {
                await GetValue(command);
                return;
            }
            if (_subCommandDictionary.ContainsKey(subcommand))
            {
                await _subCommandDictionary[subcommand].Invoke(command);
            }
        }
        private async Task GetValue(SocketSlashCommand command)
        {
            var value = await _valueStorage.GetValue(command.User.Id);
            await command.RespondAsync($"У тебя {value} денех");
        }
        private async Task GetTop(SocketSlashCommand command)
        {
            var values = await _valueStorage.GetNetwothes(0, 5, Order.ByBalance);
            var stringBuilder = new StringBuilder();
            var counter = 1;
            stringBuilder.Append("Список крутых:\n");
            foreach (var value in values)
            {
                stringBuilder.Append($"{counter++}: {(await _socketClient.GetUserAsync(value.Key)).Username} - {value.Value}\n");
            }
            await command.RespondAsync(stringBuilder.ToString());
        }
        private async Task PayTo(SocketSlashCommand command)
        {
            var amount = (long)command.Data.Options.First().Options.First().Value;
            var user = (SocketUser)command.Data.Options.First().Options.Skip(1).First().Value;

            var result = await _valueStorage.TransferValue(new Models.TransactionModel { UserIdFrom = command.User.Id, UserIdTo = user.Id, Value = amount});
            if (result)
            {
                await command.RespondAsync("Передача прошла успешно");
            }
            else
            {
                await command.RespondAsync("Передача не прошла успешно");
            }
        }
    }
}
