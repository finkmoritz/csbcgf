using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyReadyToAttackAction : IAction
    {
        [JsonProperty]
        public IMonsterCard MonsterCard;

        [JsonProperty]
        public bool IsReadyToAttack;

        [JsonConstructor]
        public ModifyReadyToAttackAction(IMonsterCard monsterCard, bool isReadyToAttack)
        {
            MonsterCard = monsterCard;
            IsReadyToAttack = isReadyToAttack;
        }

        public void Execute(IGame game)
        {
            MonsterCard.IsReadyToAttack = IsReadyToAttack;
        }

        public bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack
                && gameState.AllCardsOnTheBoard.Contains(MonsterCard);
        }
    }
}
