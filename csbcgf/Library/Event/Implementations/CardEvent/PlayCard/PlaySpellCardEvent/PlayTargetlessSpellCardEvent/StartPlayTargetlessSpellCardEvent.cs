using System;

namespace csbcgf
{
    [Serializable]
    public class StartPlayTargetlessSpellCardEvent : StartPlaySpellCardEvent
    {
        public StartPlayTargetlessSpellCardEvent(ITargetlessSpellCard spellCard)
            : base(spellCard)
        {
        }

        public StartPlayTargetlessSpellCardEvent(Func<ITargetlessSpellCard> getSpellCard)
            : base(getSpellCard)
        {
        }
    }
}
