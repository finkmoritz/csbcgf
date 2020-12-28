using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(List<ITargetlessSpellCardComponent> components)
            : base(components.ConvertAll(c => (ISpellCardComponent)c))
        {
        }

        public TargetlessSpellCard() : base()
        {
        }

        public abstract void Play(IGame game);
    }
}
