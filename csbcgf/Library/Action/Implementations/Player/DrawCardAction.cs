using System;

namespace csbcgf
{
    public class DrawCardAction : Action
    {
        public IPlayer Player;

        public ICard DrawnCard;

        public DrawCardAction(IPlayer player, bool isAborted = false)
            : base(isAborted)
        {
            Player = player;
        }

        public override void Execute(IGame game)
        {
            RemoveCardFromDeckAction removeAction = new RemoveCardFromDeckAction(Player.Deck);
            game.Execute(removeAction);
            DrawnCard = removeAction.Card;
            game.Execute(new AddCardToHandAction(Player.Hand, DrawnCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !Player.Deck.IsEmpty;
        }
    }
}
