namespace csbcgf
{
    public class AddCardToGraveyardAction : Action
    {
        public readonly IDeck Graveyard;

        public ICard Card;

        public AddCardToGraveyardAction(IDeck graveyard, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Graveyard = graveyard;
            Card = card;
        }

        public override void Execute(IGame game)
        {
            Graveyard.Push(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null && !Graveyard.Contains(Card);
        }
    }
}
