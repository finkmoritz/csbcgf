using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaOnStartOfTurnEventReaction : IReaction
    {
        public ModifyManaOnStartOfTurnEventReaction()
        {
        }

        public List<IAction> ReactTo(IGame gameState, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is StartOfTurnEvent)
            {
                int manaDelta = gameState.ActivePlayer.ManaBaseValue + 1 - gameState.ActivePlayer.ManaValue;
                reactions.Add(new ModifyManaStatAction(gameState.ActivePlayer, manaDelta, 1));
            }
            return reactions;
        }
    }
}
