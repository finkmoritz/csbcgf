namespace hearthstonestandalone
{
    public class HearthstoneGame
    {
        public List<HearthstoneHero> Heros { get; init; }

        public event EventHandler? GameStarted;

        public HearthstoneGame()
        {
            Heros = new List<HearthstoneHero>();
        }

        public void StartGame()
        {
            Console.WriteLine("Game starts");
            OnGameStarted(EventArgs.Empty);
        }

        protected virtual void OnGameStarted(EventArgs eventArgs)
        {
            GameStarted?.Invoke(this, eventArgs);
        }
    }
}
