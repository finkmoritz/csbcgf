using System;
using System.Collections.Generic;

namespace csbcgf
{
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard()
            : this(new List<ITargetlessSpellCardComponent>())
        {
        }

        public TargetlessSpellCard(ITargetlessSpellCardComponent component)
            : this(new List<ITargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard(List<ITargetlessSpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        public TargetlessSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public void Cast(IGame game)
        {
            foreach (ICardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.Cast(game);
                }
            }
        }

        public override object Clone()
        {
            List<ICardComponent> componentsClone = new List<ICardComponent>();
            Components.ForEach(c => componentsClone.Add((ICardComponent)c.Clone()));

            List<IReaction> reactionsClone = new List<IReaction>();
            Reactions.ForEach(r => reactionsClone.Add((IReaction)r.Clone()));

            return new TargetlessSpellCard(
                componentsClone,
                reactionsClone
            );
        }
    }
}
