using System;

namespace csbcgf
{
    public class AddCardToHandAction : Action
    {
        public readonly IHand Hand;

        public ICard Card;

        public AddCardToHandAction(IHand hand, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Hand = hand;
            Card = card;
        }

        public override object Clone()
        {
            return new AddCardToHandAction(
                (IHand)Hand.Clone(),
                (ICard)Card.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            Hand.Add(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null && Hand.Size < Hand.MaxSize;
        }
    }
}
