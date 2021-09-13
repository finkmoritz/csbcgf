using System;

namespace csbcgf
{
    public class RemoveCardFromHandAction : Action
    {
        public readonly IHand Hand;

        public ICard Card;

        public RemoveCardFromHandAction(IHand hand, ICard card, bool isAborted = false)
            : base(isAborted)
        {
            Hand = hand;
            Card = card;
        }

        public override void Execute(IGame game)
        {
            Hand.Remove(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Hand.Contains(Card);
        }
    }
}
