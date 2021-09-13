using System;

namespace csbcgf
{
    public class ModifyReadyToAttackAction : Action
    {
        public IMonsterCard MonsterCard;

        public bool IsReadyToAttack;

        public ModifyReadyToAttackAction(
            IMonsterCard monsterCard,
            bool isReadyToAttack,
            bool isAborted = false
            ) : base(isAborted)
        {
            MonsterCard = monsterCard;
            IsReadyToAttack = isReadyToAttack;
        }

        public override void Execute(IGame game)
        {
            MonsterCard.IsReadyToAttack = IsReadyToAttack;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack
                && gameState.AllCardsOnTheBoard.Contains(MonsterCard);
        }
    }
}
