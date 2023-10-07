namespace hearthstonestandalone
{
    public class HearthstoneDamageReceivedEvent : Event
    {
        public required HearthstoneDamage Damage { get; set; }
        public required HearthstoneHero Target { get; set; }

        public override void Execute(StateMachine stateMachine)
        {
            Target.Life -= Damage.Amount;
        }
    }
}
