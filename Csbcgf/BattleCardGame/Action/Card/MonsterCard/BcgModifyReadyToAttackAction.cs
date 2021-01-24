using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyReadyToAttackAction : Action
    {
        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonProperty]
        public bool IsReadyToAttack;

        [JsonConstructor]
        public BcgModifyReadyToAttackAction(
            IMonsterCard monsterCard,
            bool isReadyToAttack,
            bool isAborted = false
            ) : base(isAborted)
        {
            MonsterCard = monsterCard;
            IsReadyToAttack = isReadyToAttack;
        }

        public override object Clone()
        {
            return new BcgModifyReadyToAttackAction(
                (IMonsterCard)MonsterCard.Clone(),
                IsReadyToAttack,
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            MonsterCard.IsReadyToAttack = IsReadyToAttack;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack;
                //&& gameState.AllCardsOnTheBoard.Contains(MonsterCard); //TODO
        }
    }
}
