using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyReadyToAttackAction : IAction
    {
        [JsonProperty]
        private readonly IMonsterCard monsterCard;

        [JsonProperty]
        private readonly bool isReadyToAttack;

        public ModifyReadyToAttackAction(IMonsterCard monsterCard, bool isReadyToAttack)
        {
            this.monsterCard = monsterCard;
            this.isReadyToAttack = isReadyToAttack;
        }

        public void Execute(IGame game)
        {
            monsterCard.IsReadyToAttack = isReadyToAttack;
        }

        public bool IsExecutable(IGame game)
        {
            return monsterCard.IsReadyToAttack != isReadyToAttack
                && (game.ActivePlayer.Board.Contains(monsterCard)
                || game.NonActivePlayer.Board.Contains(monsterCard));
        }
    }
}
