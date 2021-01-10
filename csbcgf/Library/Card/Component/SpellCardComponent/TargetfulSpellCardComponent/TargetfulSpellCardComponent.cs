using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract List<IAction> GetActions(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGame game);
    }
}
