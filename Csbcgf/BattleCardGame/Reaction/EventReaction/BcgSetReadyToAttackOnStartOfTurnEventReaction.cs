using System;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgSetReadyToAttackOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new BcgSetReadyToAttackOnStartOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                IBcgMonsterCard monsterCard = (IBcgMonsterCard)FindParentCard(game);
                IBcgPlayer owner = monsterCard.FindParentPlayer(game);
                bool isReadyToAttack = owner == game.ActivePlayer
                    && game.ActivePlayer.Board.Contains(monsterCard);

                game.Execute(new BcgModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
