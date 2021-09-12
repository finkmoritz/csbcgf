using System;

namespace csbcgf
{
    [Serializable]
    public class SetReadyToAttackOnStartOfTurnEventReaction : Reaction
    {
        public override object Clone()
        {
            return new SetReadyToAttackOnStartOfTurnEventReaction();
        }

        public override void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                IMonsterCard monsterCard = (IMonsterCard)FindParentCard(game);
                IPlayer owner = monsterCard.FindParentPlayer(game);
                bool isReadyToAttack = owner == game.ActivePlayer
                    && game.ActivePlayer.Board.Contains(monsterCard);

                game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
