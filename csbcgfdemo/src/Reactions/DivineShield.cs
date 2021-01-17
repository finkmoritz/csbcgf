using System;
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

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            if (actionEvent.IsBefore(typeof(ModifyLifeStatAction)))
            {
                ModifyLifeStatAction a = (ModifyLifeStatAction)actionEvent.Action;
                if (a.Living == parentCard && a.Delta < 0)
                {
                    a.Delta = 0;
                    parentCard.RemoveReaction(this);
                }
            }
        }
    }
}
