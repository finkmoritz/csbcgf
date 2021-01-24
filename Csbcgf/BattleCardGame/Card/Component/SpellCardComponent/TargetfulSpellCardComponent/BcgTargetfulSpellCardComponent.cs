using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgTargetfulSpellCardComponent : CardComponent, IBcgTargetfulSpellCardComponent
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

        public abstract void Cast(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGameState gameState);
    }
}
