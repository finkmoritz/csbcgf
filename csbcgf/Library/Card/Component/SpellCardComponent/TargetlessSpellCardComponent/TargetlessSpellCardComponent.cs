using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public abstract class TargetlessSpellCardComponent : CardComponent, ITargetlessSpellCardComponent
    {
        public TargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract void Cast(IGame game);
    }
}
