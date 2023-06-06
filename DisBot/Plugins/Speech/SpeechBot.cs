using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DisBot.Abstractrions.Base;
using DisBot.Abstractrions.Interfaces;
using DisBot.Speech.Functions;

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
