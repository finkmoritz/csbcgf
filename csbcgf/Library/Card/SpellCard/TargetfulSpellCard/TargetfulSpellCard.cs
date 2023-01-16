namespace csbcgf
{
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard()
            : this(new List<ISpellCardComponent>())
        {
        }

        public TargetfulSpellCard(ISpellCardComponent component)
            : this(new List<ISpellCardComponent> { component })
        {
        }

        public TargetfulSpellCard(List<ISpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        public TargetfulSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter>? potentialTargets = null;
            foreach (ICardComponent component in Components.FindAll(c => c is ITargetful))
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
