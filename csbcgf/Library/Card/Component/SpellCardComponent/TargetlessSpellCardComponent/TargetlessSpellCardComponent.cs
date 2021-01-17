using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class TargetlessSpellCardComponent : CardComponent, ITargetlessSpellCardComponent
    {
        public TargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        [JsonConstructor]
        protected TargetlessSpellCardComponent(ManaCostStat manaCostStat,
            List<IReaction> reactions, ICard parentCard)
            : base(manaCostStat, reactions, parentCard)
        {
        }

        public abstract void Cast(IGame game);
    }
}
