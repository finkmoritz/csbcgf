using System;
using System.Collections.Generic;


namespace csbcgf
{
    [Serializable]
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard(List<ISpellCardComponent> components)
            : base(components)
        {
        }

        public TargetfulSpellCard(ISpellCardComponent component)
            : this(new List<ISpellCardComponent> { component })
        {
        }

        public TargetfulSpellCard() : base()
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = null;
            foreach (ICardComponent component in Components.FindAll(c => c is ITargetful))
            {
                if (potentialTargets == null)
                {
                    potentialTargets = ((ITargetful)component).GetPotentialTargets(game);
                }
                else
                {
                    potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(game));
                }
            }
            return potentialTargets ?? new HashSet<ICharacter>();
        }

        public List<IAction> GetActions(IGame game, ICharacter targetCharacter)
        {
            if (!GetPotentialTargets(game).Contains(targetCharacter))
            {
                throw new CsbcgfException("Tried to play a TargetfulSpellCard " +
                    "on an invalid target character!");
            }

            List<IAction> actions = new List<IAction>();
            foreach (ISpellCardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.GetActions(game).ForEach(
                        a => actions.Add(a)
                    );
                } else if (component is ITargetfulSpellCardComponent targetfulComponent)
                {
                    targetfulComponent.GetActions(game, targetCharacter).ForEach(
                        a => actions.Add(a)
                    );
                }
            }
            return actions;
        }
    }
}
