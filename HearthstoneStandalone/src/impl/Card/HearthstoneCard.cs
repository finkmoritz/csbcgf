namespace hearthstone
{
    public abstract class HearthstoneCard : HearthstoneGameObject
    {
        public int Cost { get; set; }

        public HearthstoneCard(HearthstoneStateMachine stateMachine, int cost) : base(stateMachine)
        {
            Cost = cost;
        }

        protected void Play(HearthstoneHero actor)
        {
            StateMachine.SendHearthstoneCardPlayed(new HearthstoneCardPlayedEvent { Card = this, Actor = actor });
        }
    }
}