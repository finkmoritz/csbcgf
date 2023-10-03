namespace hearthstonestandalone
{
    public class HearthstoneHero
    {
        public int Life { get; set; }
        public int Diamonds { get; set; }

        public List<HearthstoneCard> Deck { get; init; }
        public List<HearthstoneCard> Hand { get; init; }

        public HearthstoneHero()
        {
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

        public void PlaySpellCard(HearthstoneSpellCard card, HearthstoneSpellCardPlayEventArgs args)
        {
            if (card.Cost <= Diamonds)
            {
                Diamonds -= card.Cost;
                Hand.Remove(card);
                card.Play(args);
            }
            else
            {
                Console.WriteLine("Card is too expensive!"); // TODO
            }
        }

        public void OnGameStarted(object? sender, EventArgs eventArgs)
        {
            DrawCards(5);
        }

        public void OnTurnStarted(object? sender, HearthstoneHero hero)
        {
            if (this.Equals(hero))
            {
                Diamonds += 1;
                DrawCards(1);
            }
        }

        public void ReceiveDamage(int damage)
        {
            Life -= damage;
        }
    }
}