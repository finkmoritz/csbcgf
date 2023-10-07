namespace hearthstone
{
    public class HearthstoneDamageReceivedEvent : HearthstoneEvent
    {
        public required HearthstoneDamage Damage { get; set; }
        public required HearthstoneHero Target { get; set; }

        public override void Execute(HearthstoneStateMachine stateMachine)
        {
            Target.Life -= Damage.Amount;
        }
    }
}
