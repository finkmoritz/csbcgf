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

        public List<IAction> GetActions(IGame game)
        {
            List<IAction> actions = new List<IAction>();
            Components.ForEach(
                c => ((ITargetlessSpellCardComponent)c).GetActions(game).ForEach(
                    a => actions.Add(a)
                )
            );
            return actions;
        }
    }
}
