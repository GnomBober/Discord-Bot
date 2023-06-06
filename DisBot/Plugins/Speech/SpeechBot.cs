using DisBot.Abstractrions.Base;
using DisBot.Abstractrions.Interfaces;
using DisBot.Plugins.Speech.Functions;

namespace DisBot.Plugins.Speech
{
    class SpeechBot : BaseBot
    {
        public SpeechBot(string token, IEnumerable<IBotFunction> botFunctions) : base(token, botFunctions) 
        {
            _botFunctions = _botFunctions.Append(new ResponderFunction());
        }
    }
}
