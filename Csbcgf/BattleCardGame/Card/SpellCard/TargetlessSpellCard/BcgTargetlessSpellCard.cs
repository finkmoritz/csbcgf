using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgTargetlessSpellCard : BcgSpellCard, IBcgTargetlessSpellCard
    {
        public BcgTargetlessSpellCard()
            : this(new List<IBcgTargetlessSpellCardComponent>())
        {
        }

        public BcgTargetlessSpellCard(IBcgTargetlessSpellCardComponent component)
            : this(new List<IBcgTargetlessSpellCardComponent> { component })
        {
        }

        public BcgTargetlessSpellCard(List<IBcgTargetlessSpellCardComponent> components)
            : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>())
        {
        }

        [JsonConstructor]
        public BcgTargetlessSpellCard(List<ICardComponent> components, List<IReaction> reactions)
            : base(components, reactions)
        {
        }

        public void Cast(IBcgGame game)
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

            return new BcgTargetlessSpellCard(
                componentsClone,
                reactionsClone
            );
        }
    }
}
