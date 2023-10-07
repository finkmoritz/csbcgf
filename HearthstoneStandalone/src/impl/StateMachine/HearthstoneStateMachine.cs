using csbcgf;

namespace hearthstone
{
    public class HearthstoneStateMachine : StateMachine
    {
        public event EventHandler<HearthstoneGameStartedEvent>? BeforeHearthstoneGameStarted;
        public event EventHandler<HearthstoneGameStartedEvent>? AfterHearthstoneGameStarted;

        public event EventHandler<HearthstoneTurnStartedEvent>? BeforeHearthstoneTurnStarted;
        public event EventHandler<HearthstoneTurnStartedEvent>? AfterHearthstoneTurnStarted;

        public event EventHandler<HearthstoneCardPlayedEvent>? BeforeHearthstoneCardPlayed;
        public event EventHandler<HearthstoneCardPlayedEvent>? AfterHearthstoneCardPlayed;

        public event EventHandler<HearthstoneDamageReceivedEvent>? BeforeHearthstoneDamageReceived;
        public event EventHandler<HearthstoneDamageReceivedEvent>? AfterHearthstoneDamageReceived;

        public void SendHearthstoneGameStarted(HearthstoneGameStartedEvent e)
        {
            BeforeHearthstoneGameStarted?.Invoke(this, e);
            e.Execute(this);
            AfterHearthstoneGameStarted?.Invoke(this, e);
        }

        public void SendHearthstoneTurnStarted(HearthstoneTurnStartedEvent e)
        {
            BeforeHearthstoneTurnStarted?.Invoke(this, e);
            e.Execute(this);
            AfterHearthstoneTurnStarted?.Invoke(this, e);
        }

        public void SendHearthstoneCardPlayed(HearthstoneCardPlayedEvent e)
        {
            BeforeHearthstoneCardPlayed?.Invoke(this, e);
            e.Execute(this);
            AfterHearthstoneCardPlayed?.Invoke(this, e);
        }

        public void SendHearthstoneDamageReceived(HearthstoneDamageReceivedEvent e)
        {
            BeforeHearthstoneDamageReceived?.Invoke(this, e);
            e.Execute(this);
            AfterHearthstoneDamageReceived?.Invoke(this, e);
        }
    }
}
