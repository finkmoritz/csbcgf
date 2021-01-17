using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(List<ITargetlessSpellCardComponent> components)
            : this(components, null)
        {
        }

        public TargetlessSpellCard(ITargetlessSpellCardComponent component)
            : this(new List<ITargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard() : this(new List<ITargetlessSpellCardComponent>())
        {
        }

        public TargetlessSpellCard(
            List<ITargetlessSpellCardComponent> components,
            IPlayer owner
            ) : this(components.ConvertAll(c => (ICardComponent)c), owner)
        {
        }

        [JsonConstructor]
        public TargetlessSpellCard(
            List<ICardComponent> components,
            IPlayer owner
            ) : base(components, owner)
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
            ITargetlessSpellCard clone = new TargetlessSpellCard(
                new List<ITargetlessSpellCardComponent>(),
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
