using System;
namespace csbcgf
{
    [Serializable]
    public abstract class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(int mana) : base(mana)
        {
        }

        public abstract void Play(IGame game);
    }
}
