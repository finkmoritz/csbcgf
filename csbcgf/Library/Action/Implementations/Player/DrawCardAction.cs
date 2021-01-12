﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class DrawCardAction : Action
    {
        [JsonProperty]
        public IPlayer Player;

        [JsonProperty]
        public ICard DrawnCard;

        public DrawCardAction(IPlayer player)
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
