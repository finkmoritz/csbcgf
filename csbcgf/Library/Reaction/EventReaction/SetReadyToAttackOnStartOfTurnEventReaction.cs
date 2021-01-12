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

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                bool isReadyToAttack = monsterCard.Owner == game.ActivePlayer
                    && game.ActivePlayer.Board.Contains(monsterCard);
                game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
