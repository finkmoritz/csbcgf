using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class DrawCardAction : IAction
    {
        [JsonProperty]
        public IPlayer Player;

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

        public bool IsExecutable(IGameState gameState)
        {
            return !Player.Deck.IsEmpty;
        }
    }
}
