namespace hearthstone
{
    public class HearthstoneTurnStartedEvent : HearthstoneEvent
    {
        public required HearthstoneHero CurrentHero { get; set; }

        public override void Execute(HearthstoneStateMachine stateMachine)
        {

        }
    }
}
