using System;
using System.Collections.Generic;
using csbcgf;

namespace csbcgfdemo
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    public class DivineShield : IReaction
    {
        protected IMonsterCard parentCard;

        public DivineShield(IMonsterCard parentCard)
        {
            this.parentCard = parentCard;
        }

        public List<IAction> ReactTo(IGame gameState, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            if (action is ModifyLifeStatAction a
                && a.Character == parentCard
                && a.Delta < 0)
            {
                a.Delta = 0;
                parentCard.RemoveReaction(this);
            }
            return reactions;
        }
    }
}
