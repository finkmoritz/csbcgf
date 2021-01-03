using System;

namespace csbcgf
{
    [Serializable]
    public class StartPlaySpellCardEvent : StartPlayCardEvent
    {
        public StartPlaySpellCardEvent(ISpellCard spellCard) : base(spellCard)
        {
        }

        public StartPlaySpellCardEvent(Func<ISpellCard> getSpellCard) : base(getSpellCard)
        {
        }
    }
}
