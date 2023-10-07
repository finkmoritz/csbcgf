namespace hearthstonestandalone
{
    public class HearthstoneTurnStartedEvent : Event
    {
        public required HearthstoneHero CurrentHero { get; set; }

        public override void Execute(StateMachine stateMachine)
        {

        }
    }
}
