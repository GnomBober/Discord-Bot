using DisBot.Core.Shared.Config;
using DisBot.Plugins.Speech;
using DisBot.Core.Shared.BotFunctions;
using DisBot.Abstractrions.Interfaces;
using Discord.Commands;
using DisBot.Core.Shared.Services;

namespace DisBot;
public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var config = new ConfigHandler().GetConfigs();
        var commandService = new CommandStorage();
        var speechBot = new SpeechBot(config.token, new List<IBotFunction> { new LoggerFunction(), new JopaFunction(commandService), new WatermelonCoolingFunction(commandService), commandService});

        speechBot.Build();
        await speechBot.Start();

        await Task.Delay(-1);
    }
}