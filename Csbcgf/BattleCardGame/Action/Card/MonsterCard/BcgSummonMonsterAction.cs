using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgSummonMonsterAction : Core.Action
    {
        [JsonProperty]
        public IBcgMonsterCard MonsterCard;

        [JsonProperty]
        public ICardCollection Source;

        [JsonProperty]
        public ICardCollection Destination;

        [JsonConstructor]
        public BcgSummonMonsterAction(
            IBcgMonsterCard monsterCard,
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
            return new BcgSummonMonsterAction(
                (IBcgMonsterCard)MonsterCard.Clone(),
                null, // otherwise circular dependencies
                null, // otherwise circular dependencies
                IsAborted
                );
        }

        public override void Execute(IGame game)
        {
            IBcgPlayer player = MonsterCard.FindParentPlayer(game);
            game.Execute(new BcgModifyManaStatAction(player, -MonsterCard.ManaValue, 0));
            game.Execute(new TransferCardAction(MonsterCard, Source, Destination));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsSummonable(gameState);
        }
    }
}
