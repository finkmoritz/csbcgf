using csbcgf;

namespace hearthstonestandalone
{
    public class HearthstoneHero : GameObject<HearthstoneStateMachine>
    {
        public int Life { get; set; }
        public int Diamonds { get; set; }

        public List<HearthstoneCard> Deck { get; init; }
        public List<HearthstoneCard> Hand { get; init; }

        public HearthstoneHero(HearthstoneStateMachine stateMachine) : base(stateMachine)
        {
            StateMachine.HearthstoneGameStarted += OnGameStarted;
            StateMachine.HearthstoneTurnStarted += OnTurnStarted;

            Life = 20;
            Diamonds = 0;

            Deck = new List<HearthstoneCard>();
            Hand = new List<HearthstoneCard>();
        }

        public void DrawCards(int max = 1)
        {
            for (int n = 0; n < Math.Min(max, Deck.Count); ++n)
            {
                HearthstoneCard card = Deck[0];
                Deck.Remove(card);
                Hand.Add(card);
            }
        }

        public void OnGameStarted(object? _, HearthstoneGameStartedEvent __)
        {
            DrawCards(5);
        }

        public void OnTurnStarted(object? _, HearthstoneTurnStartedEvent e)
        {
            if (Equals(e.CurrentHero))
            {
                Diamonds += 1;
                DrawCards(1);
            }
        }

        public void ReceiveDamage(HearthstoneDamage damage)
        {
            StateMachine.SendHearthstoneDamageReceived(new HearthstoneDamageReceivedEvent { Damage = damage, Target = this });
        }
    }
}