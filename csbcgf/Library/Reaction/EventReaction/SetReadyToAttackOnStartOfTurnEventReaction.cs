using System;
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

        public object Clone()
        {
            return new SetReadyToAttackOnStartOfTurnEventReaction((IMonsterCard)monsterCard.Clone());
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if(actionEvent.IsAfter(typeof(StartOfTurnEvent)))
            {
                IPlayer owner = monsterCard.FindOwner(game);
                bool isReadyToAttack = owner == game.ActivePlayer
                    && game.ActivePlayer.Board.Contains(monsterCard);
                game.Execute(new ModifyReadyToAttackAction(monsterCard, isReadyToAttack));
            }
        }
    }
}
