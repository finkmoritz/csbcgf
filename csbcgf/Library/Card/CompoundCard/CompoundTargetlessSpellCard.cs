using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public class CompoundTargetlessSpellCard : CompoundCard, ITargetlessSpellCard
    {
        public CompoundTargetlessSpellCard(List<ITargetlessSpellCard> components)
            : base(new List<ICard>())
        {
            components.ForEach(c => Components.Add(c));
        }

        public CompoundTargetlessSpellCard(ITargetlessSpellCard spellCard)
            : this(new List<ITargetlessSpellCard> { spellCard })
        {
        }

        public void Play(IGame game)
        {
            Components.ForEach(c => ((ITargetlessSpellCard)c).Play(game));
        }
    }
}
