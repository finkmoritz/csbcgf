using System;

namespace csbcgf
{
    [Serializable]
    public class EndPlayTargetfulSpellCardEvent : EndPlaySpellCardEvent
    {
        public EndPlayTargetfulSpellCardEvent(ITargetfulSpellCard spellCard, ICharacter target)
            : base(spellCard)
        {
            Target = target;
        }

        public EndPlayTargetfulSpellCardEvent(Func<ITargetfulSpellCard> getSpellCard, ICharacter target)
            : base(getSpellCard)
        {
            Target = target;
        }

        public ICharacter Target { get; protected set; }
    }
}
