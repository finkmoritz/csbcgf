namespace hearthstonestandalone
{
    public class StateMachine
    {
        public event EventHandler<HearthstoneGameStartedEvent>? GameStarted;
        public event EventHandler<HearthstoneTurnStartedEvent>? TurnStarted;
        public event EventHandler<HearthstoneCardPlayedEvent>? CardPlayed;

        public virtual void OnGameStarted(HearthstoneGameStartedEvent e)
        {
            GameStarted?.Invoke(this, e);
        }

        public virtual void OnTurnStarted(HearthstoneTurnStartedEvent e)
        {
            TurnStarted?.Invoke(this, e);
        }

        public virtual void OnHearthstoneCardPlayed(HearthstoneCardPlayedEvent e)
        {
            e.Execute(this);
            CardPlayed?.Invoke(this, e);
        }
    }
}
