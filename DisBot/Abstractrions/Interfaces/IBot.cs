namespace DisBot.Abstractrions.Interfaces
{
    interface IBot
    {
        public void Build();
        public Task Start();
        public Task Stop();
    }
}
