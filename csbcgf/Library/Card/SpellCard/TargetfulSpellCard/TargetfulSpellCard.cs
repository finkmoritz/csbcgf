using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        [JsonConstructor]
        public TargetfulSpellCard(List<ISpellCardComponent> components)
            : base(components)
        {
        }

        public TargetfulSpellCard(ISpellCardComponent component)
            : this(new List<ISpellCardComponent> { component })
        {
        }

        public TargetfulSpellCard() : this(new List<ISpellCardComponent>())
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGame gameState)
        {
            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = null;
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

        public List<IAction> GetActions(IGame gameState, ICharacter target)
        {
            if (!GetPotentialTargets(gameState).Contains(target))
            {
                throw new CsbcgfException("Tried to play a TargetfulSpellCard " +
                    "on an invalid target character!");
            }

            List<IAction> actions = new List<IAction>();
            foreach (ISpellCardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.GetActions(gameState).ForEach(
                        a => actions.Add(a)
                    );
                } else if (component is ITargetfulSpellCardComponent targetfulComponent)
                {
                    targetfulComponent.GetActions(gameState, target).ForEach(
                        a => actions.Add(a)
                    );
                }
            }
            return actions;
        }
    }
}
