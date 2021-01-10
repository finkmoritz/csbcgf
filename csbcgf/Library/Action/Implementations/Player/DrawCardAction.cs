using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class DrawCardAction : IAction
    {
        public IPlayer Player { get; }

        public DrawCardAction(IPlayer player)
        {
            Player = player;
        }

        public void Execute(IGame game)
        {
            RemoveCardFromDeckAction removeAction = new RemoveCardFromDeckAction(Player.Deck);
            game.Execute(removeAction);
            game.Execute(new AddCardToHandAction(Player.Hand, removeAction.Card));
        }

        public bool IsExecutable(IGame game)
        {
            return !Player.Deck.IsEmpty;
        }
    }
}
