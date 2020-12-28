using System;
using System.Collections.Generic;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract List<IAction> GetActions(IGame game, ICharacter targetCharacter);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGame game);
    }
}
