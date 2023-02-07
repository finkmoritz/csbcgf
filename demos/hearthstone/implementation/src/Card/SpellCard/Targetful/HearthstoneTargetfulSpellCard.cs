using csbcgf;

namespace hearthstone
{
    public class HearthstoneTargetfulSpellCard : TargetfulSpellCard, IHearthstoneTargetfulSpellCard
    {
        protected HearthstoneTargetfulSpellCard()
        {
        }

        public HearthstoneTargetfulSpellCard(bool _ = true) : base(_)
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
