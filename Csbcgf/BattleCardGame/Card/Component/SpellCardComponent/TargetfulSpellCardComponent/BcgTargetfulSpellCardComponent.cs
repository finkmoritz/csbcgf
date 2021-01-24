using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgTargetfulSpellCardComponent : BcgCardComponent, IBcgTargetfulSpellCardComponent
    {
        public BcgTargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        [JsonConstructor]
        protected BcgTargetfulSpellCardComponent(BcgManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(manaCostStat, reactions)
        {
        }

        public abstract void Cast(IGame game, IBcgCharacter target);

        public abstract HashSet<IBcgCharacter> GetPotentialTargets(IGameState gameState);
    }
}
