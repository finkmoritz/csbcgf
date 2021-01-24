using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgTargetlessSpellCardComponent : BcgCardComponent, IBcgTargetlessSpellCardComponent
    {
        public BcgTargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        [JsonConstructor]
        protected BcgTargetlessSpellCardComponent(BcgManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(manaCostStat, reactions)
        {
        }

        public abstract void Cast(IBcgGame game);
    }
}
