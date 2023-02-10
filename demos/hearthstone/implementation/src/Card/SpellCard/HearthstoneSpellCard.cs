using csbcgf;

namespace hearthstone
{
    public class HearthstoneSpellCard : Card
    {
        protected HearthstoneSpellCard()
        {
        }

        public HearthstoneSpellCard(bool _ = true) : base(_)
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
