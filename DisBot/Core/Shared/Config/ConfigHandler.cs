using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
