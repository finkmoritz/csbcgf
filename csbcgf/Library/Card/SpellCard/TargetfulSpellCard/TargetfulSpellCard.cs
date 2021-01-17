using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard(List<ISpellCardComponent> components)
            : this(components, null)
        {
        }

        public TargetfulSpellCard(ISpellCardComponent component)
            : this(new List<ISpellCardComponent> { component })
        {
        }

        public TargetfulSpellCard() : this(new List<ISpellCardComponent>())
        {
        }

        public TargetfulSpellCard(List<ISpellCardComponent> components,
            IPlayer owner
            ) : this(components.ConvertAll(c => (ICardComponent)c), owner)
        {
        }

        [JsonConstructor]
        public TargetfulSpellCard(List<ICardComponent> components,
            IPlayer owner
            ) : base(components, owner)
        {
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
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

        public override object Clone()
        {
            ITargetfulSpellCard clone = new TargetfulSpellCard(
                new List<ISpellCardComponent>(),
                null // otherwise circular dependencies
            );
            foreach (ICardComponent c in Components)
            {
                ICardComponent cc = (ICardComponent)c.Clone();
                cc.ParentCard = clone;
                clone.AddComponent(cc);
            }
            return clone;
        }
    }
}
