namespace hearthstonestandalone
{
    public abstract class HearthstoneCard : StatefulGameObject
    {
        public int Cost { get; set; }

        public HearthstoneCard(StateMachine stateMachine, int cost) : base(stateMachine)
        {
            Cost = cost;
        }

        protected void Play(HearthstoneHero actor)
        {
            StateMachine.OnHearthstoneCardPlayed(new HearthstoneCardPlayedEvent { Card = this, Actor = actor });
        }
    }
}