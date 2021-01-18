using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(ITargetlessSpellCardComponent component)
            : this(new List<ITargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard() : this(new List<ITargetlessSpellCardComponent>())
        {
        }

        public TargetlessSpellCard(
            List<ITargetlessSpellCardComponent> components
            ) : this(components.ConvertAll(c => (ICardComponent)c))
        {
        }

        [JsonConstructor]
        public TargetlessSpellCard(List<ICardComponent> components)
            : base(components)
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

            return new TargetlessSpellCard(
                componentsClone
            );
        }
    }
}
