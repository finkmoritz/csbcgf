using System;
namespace csbcgf
{
    [Serializable]
    public class EndPlaySpellCardEvent : EndPlayCardEvent
    {
        public EndPlaySpellCardEvent(ISpellCard spellCard) : base(spellCard)
        {
        }

        public EndPlaySpellCardEvent(Func<ISpellCard> getSpellCard) : base(getSpellCard)
        {
        }
    }
}
