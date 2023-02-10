using csbcgf;

namespace hearthstone
{
    public class HearthstoneTargetlessSpellCard : HearthstoneSpellCard
    {
        protected HearthstoneTargetlessSpellCard()
        {
        }

        public HearthstoneTargetlessSpellCard(bool _ = true) : base(_)
        {
        }

        public void Cast(HearthstoneGame game)
        {
            foreach (HearthstoneTargetlessSpellCardComponent component in Components)
            {
                component.Cast(game);
            }
        }
    }
}
