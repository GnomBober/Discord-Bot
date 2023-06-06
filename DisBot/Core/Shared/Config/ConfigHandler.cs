using Microsoft.Extensions.Configuration;

namespace DisBot.Core.Shared.Config
{
    class ConfigHandler
    {
        public Configs GetConfigs()
        {
            IConfiguration tokenConfig = new ConfigurationBuilder()
            .AddJsonFile("json1.json")
            .AddEnvironmentVariables()
            .Build();

            return tokenConfig.GetRequiredSection("Configs").Get<Configs>()!;
        }
    }
}
