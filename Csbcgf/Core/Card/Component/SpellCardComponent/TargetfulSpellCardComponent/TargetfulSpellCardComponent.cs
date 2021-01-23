using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        [JsonConstructor]
        protected TargetfulSpellCardComponent(ManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(manaCostStat, reactions)
        {
        }

        public abstract void Cast(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGameState gameState);
    }
}
