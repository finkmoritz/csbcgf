using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard()
            : this(new List<IBcgTargetlessSpellCardComponent>())
        {
        }

        public TargetlessSpellCard(IBcgTargetlessSpellCardComponent component)
            : this(new List<IBcgTargetlessSpellCardComponent> { component })
        {
        }

        public TargetlessSpellCard(List<IBcgTargetlessSpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public TargetlessSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public void Cast(IGame game)
        {
            foreach (ICardComponent component in Components)
            {
                if (component is IBcgTargetlessSpellCardComponent targetlessComponent)
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
