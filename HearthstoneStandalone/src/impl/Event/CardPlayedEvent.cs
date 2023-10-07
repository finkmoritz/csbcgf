namespace hearthstonestandalone
{
    public class HearthstoneCardPlayedEvent : Event
    {
        public required HearthstoneCard Card { get; set; }
        public required HearthstoneHero Actor { get; set; }

        public override void Execute(StateMachine stateMachine)
        {
            if (Card.Cost <= Actor.Diamonds)
            {
                Actor.Diamonds -= Card.Cost;
                Actor.Hand.Remove(Card);
            }
            else
            {
                Console.WriteLine("Card is too expensive!"); // TODO
            }
        }
    }
}
