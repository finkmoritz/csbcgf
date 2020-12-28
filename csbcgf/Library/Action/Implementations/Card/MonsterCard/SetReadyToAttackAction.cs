using System;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class SetReadyToAttackAction : IAction
    {
        private IMonsterCard monsterCard;
        private bool isReadyToAttack;

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
