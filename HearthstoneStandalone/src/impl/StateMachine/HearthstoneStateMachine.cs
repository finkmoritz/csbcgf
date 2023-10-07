using csbcgf;

namespace hearthstonestandalone
{
    public class HearthstoneStateMachine : StateMachine
    {
        public event EventHandler<HearthstoneGameStartedEvent>? HearthstoneGameStarted;
        public event EventHandler<HearthstoneTurnStartedEvent>? HearthstoneTurnStarted;
        public event EventHandler<HearthstoneCardPlayedEvent>? HearthstoneCardPlayed;
        public event EventHandler<HearthstoneDamageReceivedEvent>? HearthstoneDamageReceived;

        public virtual void SendHearthstoneGameStarted(HearthstoneGameStartedEvent e)
        {
            e.Execute(this);
            HearthstoneGameStarted?.Invoke(this, e);
        }

        public virtual void SendHearthstoneTurnStarted(HearthstoneTurnStartedEvent e)
        {
            e.Execute(this);
            HearthstoneTurnStarted?.Invoke(this, e);
        }

        public virtual void SendHearthstoneCardPlayed(HearthstoneCardPlayedEvent e)
        {
            e.Execute(this);
            HearthstoneCardPlayed?.Invoke(this, e);
        }

        public virtual void SendHearthstoneDamageReceived(HearthstoneDamageReceivedEvent e)
        {
            e.Execute(this);
            HearthstoneDamageReceived?.Invoke(this, e);
        }
    }
}
