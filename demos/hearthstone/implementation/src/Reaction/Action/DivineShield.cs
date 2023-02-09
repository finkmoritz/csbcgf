using csbcgf;

namespace hearthstone
{
    /// <summary>
    /// Divine Shield blocks first damage.
    /// </summary>
    public class DivineShield : CardReaction<HearthstoneGameState, HearthstoneGame, ModifyLifeStatAction<HearthstoneGameState>>
    {
        public override void ReactBefore(HearthstoneGame game, ModifyLifeStatAction<HearthstoneGameState> action)
        {
            if (action.Living == parentCard && action.Delta < 0)
            {
                action.Delta = 0;
                parentCard.RemoveReaction(this);
            }
        }
    }
}
