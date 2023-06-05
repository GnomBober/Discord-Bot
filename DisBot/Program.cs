using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using DisBot.Speech;


namespace DisBot;
public class Program
{
    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client = new DiscordSocketClient();

    public async Task MainAsync()
    {
        var disConfig = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All
        };
        _client = new DiscordSocketClient(disConfig);
        BotResponder botResponder = new BotResponder();
        _client.MessageReceived += botResponder.Greatings;
        _client.Log += Log;
        
        IConfiguration tokenConfig = new ConfigurationBuilder()
    .AddJsonFile("json1.json")
    .AddEnvironmentVariables()
    .Build();
        Configs settings = tokenConfig.GetRequiredSection("Configs").Get<Configs>()!;

        await _client.LoginAsync(TokenType.Bot, settings.token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }   
}