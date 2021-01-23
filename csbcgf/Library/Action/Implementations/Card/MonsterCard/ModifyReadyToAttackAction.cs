using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyReadyToAttackAction : Action
    {
        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonProperty]
        public bool IsReadyToAttack;

        [JsonConstructor]
        public ModifyReadyToAttackAction(
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
            return new ModifyReadyToAttackAction(
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
