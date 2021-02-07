using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
{
    [Serializable]
    public class DrawCardAction : Core.Action
    {
        [JsonProperty]
        public IBcgPlayer Player;

        [JsonConstructor]
        public DrawCardAction(IBcgPlayer player)
        {
            Player = player;
        }

        public override object Clone()
        {
            return new DrawCardAction(
                null // otherwise circular dependency
            );
        }

        public override void Execute(IGame game)
        {
            ICard card = Player.CardCollections[SimpleBcgPlayer.CardCollectionKeyDeck][0];
            game.Execute(new TransferCardAction(
                card,
                Player.CardCollections[SimpleBcgPlayer.CardCollectionKeyDeck],
                Player.CardCollections[SimpleBcgPlayer.CardCollectionKeyHand]
            ));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !Player.CardCollections[SimpleBcgPlayer.CardCollectionKeyDeck].IsEmpty;
        }
    }
}
