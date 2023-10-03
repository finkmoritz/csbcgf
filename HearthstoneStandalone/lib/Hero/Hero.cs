namespace hearthstonestandalone
{
    public class HearthstoneHero
    {
        public int Life { get; set; }

        public List<HearthstoneCard> Deck { get; init; }
        public List<HearthstoneCard> Hand { get; init; }

        public HearthstoneHero(int life)
        {
            Life = life;

            Deck = new List<HearthstoneCard>();
            Hand = new List<HearthstoneCard>();
        }

        public void DrawCards(int max = 1)
        {
            for (int n = 0; n < Math.Min(max, Deck.Count); ++n)
            {
                Console.WriteLine("Draw card");
                HearthstoneCard card = Deck[0];
                Deck.Remove(card);
                Hand.Add(card);
            }
        }

        public void OnGameStarted(object? sender, EventArgs eventArgs)
        {
            Console.WriteLine("Hero knows that game started");
            DrawCards(5);
        }
    }
}