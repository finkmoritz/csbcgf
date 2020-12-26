using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public abstract class TargetfulSpellCard : SpellCard, ITargetfulSpellCard
    {
        public TargetfulSpellCard(int mana) : base(mana)
        {
        }

        public abstract HashSet<ICharacter> GetPotentialTargets(IGame game);

        public abstract void Play(IGame game, ICharacter targetCharacter);
    }
}
