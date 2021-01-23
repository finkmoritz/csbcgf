using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class CastMonsterAction : Action
    {
        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonProperty]
        public ICardCollection Source;

        [JsonProperty]
        public ICardCollection Destination;

        [JsonConstructor]
        public CastMonsterAction(
            IMonsterCard monsterCard,
            ICardCollection source,
            ICardCollection destination,
            bool isAborted = false
            ) : base(isAborted)
        {
            MonsterCard = monsterCard;
            Source = source;
            Destination = destination;
        }

        public override object Clone()
        {
            return new CastMonsterAction(
                (IMonsterCard)MonsterCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                IsAborted
                );
        }

        public override void Execute(IGame game)
        {
            IPlayer player = MonsterCard.FindParentPlayer(game);
            game.Execute(new ModifyManaStatAction(player, -MonsterCard.ManaValue, 0));
            game.Execute(new TransferCardAction(MonsterCard, Source, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState);
        }
    }
}
