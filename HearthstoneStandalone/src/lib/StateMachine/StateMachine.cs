namespace hearthstonestandalone
{
    public class StateMachine
    {
        public event EventHandler? GameStarted;
        public event EventHandler<HearthstoneHero>? TurnStarted;
        public event EventHandler<HearthstoneCardPlayedEvent>? CardPlayed;

        public virtual void OnGameStarted()
        {
            GameStarted?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnTurnStarted(HearthstoneHero hero)
        {
            TurnStarted?.Invoke(this, hero);
        }

        public virtual void OnHearthstoneCardPlayed(HearthstoneCardPlayedEvent e)
        {
            e.Execute(this);
            CardPlayed?.Invoke(this, e);
        }
    }
}
