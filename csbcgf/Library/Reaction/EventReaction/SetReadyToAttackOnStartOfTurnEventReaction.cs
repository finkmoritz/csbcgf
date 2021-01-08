using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class SetReadyToAttackOnStartOfTurnEventReaction : IReaction
    {
        [JsonProperty]
        protected readonly IMonsterCard monsterCard;

        [JsonConstructor]
        public SetReadyToAttackOnStartOfTurnEventReaction(IMonsterCard monsterCard)
        {
            this.monsterCard = monsterCard;
        }

        public List<IAction> ReactTo(IGame gameState, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if(action is StartOfTurnEvent
                && monsterCard.Owner == gameState.ActivePlayer
                && gameState.ActivePlayer.Board.Contains(monsterCard))
            {
                reactions.Add(new ModifyReadyToAttackAction(monsterCard, true));
            }
            return reactions;
        }
    }
}
