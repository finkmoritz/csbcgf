using csbcgf;

namespace hearthstone
{
    public class HearthstoneTargetlessSpellCard : TargetlessSpellCard, IHearthstoneTargetlessSpellCard
    {
        protected HearthstoneTargetlessSpellCard()
        {
        }

        public HearthstoneTargetlessSpellCard(bool _ = true) : base(_)
        {
        }

        public override bool IsCastable(IGameState gameState)
        {
            return base.IsCastable(gameState)
                && Owner != null
                && Owner.GetCardCollection(CardCollectionKeys.Hand).Contains(this);
        }
    }
}
