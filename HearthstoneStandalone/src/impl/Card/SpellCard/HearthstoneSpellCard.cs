namespace hearthstonestandalone
{
    public abstract class HearthstoneSpellCard : HearthstoneCard
    {
        public HearthstoneSpellCard(HearthstoneStateMachine stateMachine, int cost)
            : base(stateMachine, cost)
        {

        }
    }
}