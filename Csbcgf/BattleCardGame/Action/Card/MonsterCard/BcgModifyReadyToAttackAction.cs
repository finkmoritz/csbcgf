using System;
using Newtonsoft.Json;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyReadyToAttackAction : Core.Action
    {
        [JsonProperty]
        public IBcgMonsterCard MonsterCard;

        [JsonProperty]
        public bool IsReadyToAttack;

        [JsonConstructor]
        public BcgModifyReadyToAttackAction(
            IBcgMonsterCard monsterCard,
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
                (IBcgMonsterCard)MonsterCard.Clone(),
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
