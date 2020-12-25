using System;
namespace csccgl
{
    [Serializable]
    public abstract class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        public TargetlessSpellCard(int mana) : base(mana)
        {
        }

        public abstract void Play(Game game);
    }
}
