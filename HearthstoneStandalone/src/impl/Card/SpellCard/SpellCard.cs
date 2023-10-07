namespace hearthstonestandalone
{
    public abstract class HearthstoneSpellCard : HearthstoneCard
    {
        public HearthstoneSpellCard(StateMachine stateMachine, int cost)
            : base(stateMachine, cost)
        {

        }
    }
}