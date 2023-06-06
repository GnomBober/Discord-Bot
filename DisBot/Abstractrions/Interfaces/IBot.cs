using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisBot.Abstractrions.Interfaces
{
    interface IBot
    {
        public void Build();
        public Task Start();
        public Task Stop();
    }
}
