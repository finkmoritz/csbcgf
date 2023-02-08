namespace csbcgf
{
    public class TargetfulSpellCard<T> : SpellCard, ITargetfulSpellCard<T> where T : IGameState
    {
        protected TargetfulSpellCard() : base() { }

        public TargetfulSpellCard(bool _ = true) : base(_) { }

        ISet<ICharacter> ITargetful.GetPotentialTargets(IGameState gameState)
        {
            if (gameState is T s)
            {
                return GetPotentialTargets(s);
            }
            else
            {
                return new HashSet<ICharacter>();
            }
        }

        public virtual ISet<ICharacter> GetPotentialTargets(T gameState)
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

        public virtual void Cast(IGame<T> game, ICharacter target)
        {
            if (!GetPotentialTargets(game.State).Contains(target))
            {
                throw new CsbcgfException("Tried to play a TargetfulSpellCard " +
                    "on an invalid target character!");
            }

            foreach (ICardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent<T> targetlessComponent)
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
