using csbcgf;

namespace hearthstonestandalone
{
    public abstract class HearthstoneCard : GameObject<HearthstoneStateMachine>
    {
        public int Cost { get; set; }

        public HearthstoneCard(HearthstoneStateMachine stateMachine, int cost) : base(stateMachine)
        {
            Cost = cost;
        }

        protected void Play(HearthstoneHero actor)
        {
            StateMachine.OnHearthstoneCardPlayed(new HearthstoneCardPlayedEvent { Card = this, Actor = actor });
        }
    }
}