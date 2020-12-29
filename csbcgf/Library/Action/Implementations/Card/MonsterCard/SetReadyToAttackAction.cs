using System;

namespace csbcgf
{
    [Serializable]
    public class SetReadyToAttackAction : IAction
    {
        private readonly IMonsterCard monsterCard;
        private readonly bool isReadyToAttack;

        public SetReadyToAttackAction(IMonsterCard monsterCard, bool isReadyToAttack)
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
