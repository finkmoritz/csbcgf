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

        public abstract List<ICharacter> GetPotentialTargets(Game game);

        public abstract void Play(Game game, ICharacter targetCharacter);
    }
}
