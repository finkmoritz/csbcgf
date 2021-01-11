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

        public bool IsExecutable(IGame game)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack
                && game.AllCardsOnTheBoard.Contains(MonsterCard);
        }
    }
}
