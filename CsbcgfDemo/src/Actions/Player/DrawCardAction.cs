using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class DrawCardAction : Action
    {
        [JsonProperty]
        public IPlayer Player;

        [JsonProperty]
        public ICard DrawnCard;

        [JsonConstructor]
        public DrawCardAction(IPlayer player, bool isAborted = false)
            : base(isAborted)
        {
            Player = player;
        }

        public override object Clone()
        {
            return new DrawCardAction(
                null, // otherwise circular dependencies
                IsAborted
            );
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
