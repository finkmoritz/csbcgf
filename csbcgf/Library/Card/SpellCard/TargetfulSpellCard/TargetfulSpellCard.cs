namespace csbcgf
{
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        protected TargetfulSpellCard() : base() { }

        public TargetfulSpellCard(bool initialize = true) : base(initialize) { }

        public ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            //Compute the intersection of all potential targets
            ISet<ICharacter>? potentialTargets = null;
            foreach (ICardComponent component in Components.Where(c => c is ITargetful))
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((ITargetful)component).GetPotentialTargets(gameState);
                }
                else
                {
                    potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(gameState));
                }
            }
            return potentialTargets ?? new HashSet<ICharacter>();
        }

        public void Cast(IGame game, ICharacter target)
        {
            if (!GetPotentialTargets(game).Contains(target))
            {
                throw new CsbcgfException("Tried to play a TargetfulSpellCard " +
                    "on an invalid target character!");
            }

            foreach (ICardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.Cast(game);
                }
                else if (component is ITargetfulSpellCardComponent targetfulComponent)
                {
                    targetfulComponent.Cast(game, target);
                }
            }
        }
    }
}
