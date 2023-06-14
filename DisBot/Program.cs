using DisBot.Core.Shared.Config;
using DisBot.Plugins.Speech;
using DisBot.Core.Shared.BotFunctions;
using DisBot.Abstractrions.Interfaces;
using DisBot.Plugins.ValueBot;
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
        var valuebot = new ValueBot(config.token, commandService, new List<IBotFunction>() { new LoggerFunction() });
        speechBot.Build();
        valuebot.Build();

        await valuebot.Start();
        await speechBot.Start();

        await Task.Delay(-1);
    }
}