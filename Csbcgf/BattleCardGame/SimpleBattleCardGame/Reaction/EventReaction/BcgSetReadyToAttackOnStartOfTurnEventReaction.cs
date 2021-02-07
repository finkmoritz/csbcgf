using System;
using Csbcgf.BattleCardGame;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame.SimpleBattleCardGame
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
                IBcgPlayer owner = (IBcgPlayer)monsterCard.FindParentPlayer(game);
                bool isReadyToAttack = owner == ((SimpleBcgGame)game).ActivePlayer
                    && owner.CardCollections[SimpleBcgPlayer.CardCollectionKeyBoard].Contains(monsterCard);

                game.Execute(new BcgModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
