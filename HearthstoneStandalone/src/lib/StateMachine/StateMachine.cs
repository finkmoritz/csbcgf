namespace hearthstonestandalone
{
    public class StateMachine
    {
        public event EventHandler<HearthstoneGameStartedEvent>? GameStarted;
        public event EventHandler<HearthstoneTurnStartedEvent>? TurnStarted;
        public event EventHandler<HearthstoneCardPlayedEvent>? CardPlayed;
        public event EventHandler<HearthstoneDamageReceivedEvent>? DamageReceived;

        public virtual void OnGameStarted(HearthstoneGameStartedEvent e)
        {
            e.Execute(this);
            GameStarted?.Invoke(this, e);
        }

        public virtual void OnTurnStarted(HearthstoneTurnStartedEvent e)
        {
            e.Execute(this);
            TurnStarted?.Invoke(this, e);
        }

        public virtual void OnHearthstoneCardPlayed(HearthstoneCardPlayedEvent e)
        {
            e.Execute(this);
            CardPlayed?.Invoke(this, e);
        }

        public virtual void OnHearthstoneDamageReceived(HearthstoneDamageReceivedEvent e)
        {
            e.Execute(this);
            DamageReceived?.Invoke(this, e);
        }
    }
}
