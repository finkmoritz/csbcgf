using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class DieAction : Action
    {
        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonConstructor]
        public DieAction(IMonsterCard monsterCard)
        {
            MonsterCard = monsterCard;
        }

        public override void Execute(IGame game)
        {
            game.Execute(new RemoveCardFromBoardAction(MonsterCard.Owner.Board, MonsterCard));
            game.Execute(new AddCardToGraveyardAction(MonsterCard.Owner.Graveyard, MonsterCard));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.Owner.Board.Contains(MonsterCard);
        }
    }
}
