using csbcgf;

namespace hearthstone
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    public class DivineShield : CardReaction<ModifyLifeStatAction>
    {
        public override void ReactBefore(IGame game, ModifyLifeStatAction action)
        {
            if (action.Living == parentCard && action.Delta < 0)
            {
                action.Delta = 0;
                parentCard.RemoveReaction(this);
            }
        }
    }
}
