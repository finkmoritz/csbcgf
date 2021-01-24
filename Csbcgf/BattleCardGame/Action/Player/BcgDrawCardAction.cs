using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgDrawCardAction : Core.Action
    {
        [JsonProperty]
        public IBcgPlayer Player;

        [JsonConstructor]
        public BcgDrawCardAction(IBcgPlayer player)
        {
            Player = player;
        }

        public override object Clone()
        {
            return new BcgDrawCardAction(
                null // otherwise circular dependency
            );
        }

        public override void Execute(IGame game)
        {
            ICard card = Player.CardCollections[BcgPlayer.CardCollectionKeyDeck][0];
            game.Execute(new TransferCardAction(
                card,
                Player.CardCollections[BcgPlayer.CardCollectionKeyDeck],
                Player.CardCollections[BcgPlayer.CardCollectionKeyHand]
            ));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !Player.CardCollections[BcgPlayer.CardCollectionKeyDeck].IsEmpty;
        }
    }
}
