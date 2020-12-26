using System;
using System.Collections.Generic;

namespace csccgl
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
