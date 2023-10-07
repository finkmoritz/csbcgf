using csbcgf;

namespace hearthstonestandalone
{
    public class HearthstoneDamageReceivedEvent : Event<HearthstoneStateMachine>
    {
        public required HearthstoneDamage Damage { get; set; }
        public required HearthstoneHero Target { get; set; }

        public override void Execute(HearthstoneStateMachine stateMachine)
        {
            Target.Life -= Damage.Amount;
        }
    }
}
