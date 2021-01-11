using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        [JsonConstructor]
        public TargetlessSpellCard(List<ITargetlessSpellCardComponent> components)
            : base(components.ConvertAll(c => (ISpellCardComponent)c))
        {
        }

        public TargetlessSpellCard(ITargetlessSpellCardComponent component)
            : this(new List<ITargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard() : this(new List<ITargetlessSpellCardComponent>())
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
    }
}
