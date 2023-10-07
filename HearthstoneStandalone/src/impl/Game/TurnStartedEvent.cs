using csbcgf;

namespace hearthstonestandalone
{
    public class HearthstoneTurnStartedEvent : Event<HearthstoneStateMachine>
    {
        public required HearthstoneHero CurrentHero { get; set; }

        public override void Execute(HearthstoneStateMachine stateMachine)
        {

        }
    }
}
