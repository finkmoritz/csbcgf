using System;
using csbcgf;

namespace csbcgf
{
    public class StartPlayCardEvent : CardEvent
    {
        public StartPlayCardEvent(ICard card) : base(card)
        {
        }

        public StartPlayCardEvent(Func<ICard> getCard) : base(getCard)
        {
        }
    }
}
