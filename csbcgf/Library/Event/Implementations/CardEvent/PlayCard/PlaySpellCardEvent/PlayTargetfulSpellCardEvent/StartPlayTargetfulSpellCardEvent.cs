using System;

namespace csbcgf
{
    [Serializable]
    public class StartPlayTargetfulSpellCardEvent : StartPlaySpellCardEvent
    {
        public StartPlayTargetfulSpellCardEvent(ITargetfulSpellCard spellCard, ICharacter target)
            : base(spellCard)
        {
            this.target = target;
        }

        public StartPlayTargetfulSpellCardEvent(Func<ITargetfulSpellCard> getSpellCard, ICharacter target)
            : base(getSpellCard)
        {
            this.target = target;
        }

        public ICharacter target { get; protected set; }
    }
}
