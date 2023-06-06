using DisBot.Core.Shared.Config;
using DisBot.Plugins.Speech;
using DisBot.Core.Shared.BotFunctions;
using DisBot.Abstractrions.Interfaces;

namespace DisBot;
public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        var config = new ConfigHandler().GetConfigs();
        var speechBot = new SpeechBot(config.token, new List<IBotFunction> { new LoggerFunction(), new WatermelonCoolingFunction()});

        speechBot.Build();
        await speechBot.Start();

        await Task.Delay(-1);
    }
}