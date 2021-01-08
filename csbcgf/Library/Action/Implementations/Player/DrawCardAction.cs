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
            game.Execute(new List<IAction>
            {
                new StartDrawCardEvent(),
                removeAction,
                new AddCardToHandAction(Player.Hand, () => removeAction.Card),
                new EndDrawCardEvent(() => removeAction.Card)
            });
        }

        public bool IsExecutable(IGame game)
        {
            return !Player.Deck.IsEmpty;
        }
    }
}
