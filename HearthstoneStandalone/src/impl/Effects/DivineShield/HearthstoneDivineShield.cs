namespace hearthstone
{

    /// <summary>
    /// Divine Shield protects the target from the first incoming damage of any amount and afterwards vanishes.
    /// </summary>
    public class HearthstoneDivineShield : HearthstoneGameObject
    {
        public HearthstoneHero Target { get; init; } // TODO generify

        public HearthstoneDivineShield(HearthstoneStateMachine stateMachine, HearthstoneHero target) : base(stateMachine)
        {
            Target = target;
            StateMachine.BeforeHearthstoneDamageReceived += BeforeDamageReceivedEvent;
        }

        protected void BeforeDamageReceivedEvent(object? _, HearthstoneDamageReceivedEvent e)
        {
            if (e.Target.Equals(Target) && e.Damage.Amount > 0) // before this character receives non-negative damage
            {
                e.Damage.Amount = 0; // reduce the damage to zero
                Target.Components.Remove(this); // remove the divine shield component
                StateMachine.BeforeHearthstoneDamageReceived -= BeforeDamageReceivedEvent; // remove listener
            }
        }
    }
}
