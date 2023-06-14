using DisBot.Abstractrions.Base;
using DisBot.Abstractrions.Interfaces;
using DisBot.Core.Shared.Services;
using DisBot.Plugins.ValueBot.Abstractions;
using DisBot.Plugins.ValueBot.Functions;
using DisBot.Plugins.ValueBot.Services;

namespace DisBot.Plugins.ValueBot
{

    class ValueBot : BaseBot
    {
        private IValueStorage _valueStorage;
        public ValueBot(string token, CommandStorage commandStorage, IEnumerable<IBotFunction> botFunctions) : base(token, botFunctions)
        {
            _valueStorage = StubValueStorage.GetInstance();
            _botFunctions = _botFunctions.Append(new ValueCommandsFunction(commandStorage, _valueStorage));
        }
    }
}
