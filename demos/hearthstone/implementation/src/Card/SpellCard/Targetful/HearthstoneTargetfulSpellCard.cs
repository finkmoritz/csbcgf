using csbcgf;

namespace hearthstone
{
    public class HearthstoneTargetfulSpellCard : HearthstoneSpellCard
    {
        protected HearthstoneTargetfulSpellCard()
        {
        }

        public HearthstoneTargetfulSpellCard(bool _ = true) : base(_)
        {
        }

        public ISet<IStatContainer> GetPotentialTargets(HearthstoneGameState gameState)
        {
            //Compute the intersection of all potential targets
            ISet<IStatContainer>? potentialTargets = null;
            foreach (HearthstoneTargetfulSpellCardComponent component in Components)
            {
                if (potentialTargets == null)
                {
                    potentialTargets = component.GetPotentialTargets(gameState);
                }
                else
                {
                    potentialTargets.IntersectWith(component.GetPotentialTargets(gameState));
                }
            }
            return potentialTargets ?? new HashSet<IStatContainer>();
        }

        public void Cast(HearthstoneGame game, IStatContainer target)
        {
            foreach (HearthstoneTargetfulSpellCardComponent component in Components)
            {
                component.Cast(game, target);
            }
        }
    }
}
