namespace hearthstonestandalone
{
    public class HearthstoneGame
    {
        public List<HearthstoneHero> Heros { get; init; }
        public int CurrentHeroIndex { get; init; }

        public event EventHandler? GameStarted;
        public event EventHandler<HearthstoneHero>? TurnStarted;

        public HearthstoneGame()
        {
            Heros = new List<HearthstoneHero>();
            CurrentHeroIndex = 0;
        }

        public void StartGame()
        {
            OnGameStarted(EventArgs.Empty);
        }

        protected virtual void OnGameStarted(EventArgs eventArgs)
        {
            GameStarted?.Invoke(this, eventArgs);
        }

        public void NextTurn()
        {
            OnTurnStarted(Heros[CurrentHeroIndex]);
        }

        protected virtual void OnTurnStarted(HearthstoneHero hero)
        {
            TurnStarted?.Invoke(this, hero);
        }
    }
}
