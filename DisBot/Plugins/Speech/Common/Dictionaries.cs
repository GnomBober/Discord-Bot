using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisBot.Speech.Common
{
    public static class Dictionaries
    {
        public static Dictionary<ulong, string> greatingPhrases = new Dictionary<ulong, string>()
        {
            {350967741613735936, "Приветсвую, господин..."},
            {1089169753555341373, "Приветсвую, госпожа..." }
        };

        public static Dictionary<int, string> itsMe = new Dictionary<int, string>()
        {
            {1, "Да-да, я" },
            {2, "Ну я за него" },
            {3, "Присутствует" },
            {4, "На месте" },
            {5, "Чё" }
        };
    }
}
