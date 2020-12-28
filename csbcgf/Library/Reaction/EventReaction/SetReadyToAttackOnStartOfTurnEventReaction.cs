using System;
using System.Collections.Generic;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class SetReadyToAttackOnStartOfTurnEventReaction : IReaction
    {
        private readonly IMonsterCard monsterCard;

        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard)
        {
            this.monsterCard = monsterCard;
        }

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if(action is StartOfTurnEvent
                && monsterCard.Owner == game.ActivePlayer
                && game.ActivePlayer.Board.Contains(monsterCard))
            {
                reactions.Add(new SetReadyToAttackAction(monsterCard, true));
            }
            return reactions;
        }
    }
}
