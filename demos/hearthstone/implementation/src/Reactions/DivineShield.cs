using csbcgf;

namespace csbcgfdemo
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    public class DivineShield : CardReaction<ModifyLifeStatAction>
    {
        protected override void ReactBeforeInternal(IGame game, ModifyLifeStatAction action)
        {
            if (action.Living == parentCard && action.Delta < 0)
            {
                action.Delta = 0;
                parentCard.RemoveReaction(this);
            }
        }
    }
}
