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

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is StartOfTurnEvent)
            {
                int manaDelta = game.ActivePlayer.ManaBaseValue + 1 - game.ActivePlayer.ManaValue;
                reactions.Add(new ModifyManaStatAction(game.ActivePlayer, manaDelta, 1));
            }
            return reactions;
        }
    }
}
