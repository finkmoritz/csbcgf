using System;
using System.Collections.Generic;

namespace csbcgf
{
    public class CompoundTargetlessSpellCard : CompoundCard, ITargetlessSpellCard
    {
        public CompoundTargetlessSpellCard(List<ITargetlessSpellCard> components) : base(new List<ICard>())
        {
            components.ForEach(c => Components.Add(c));
        }

        public void Play(IGame game)
        {
            Components.ForEach(c => ((ITargetlessSpellCard)c).Play(game));
        }
    }
}
