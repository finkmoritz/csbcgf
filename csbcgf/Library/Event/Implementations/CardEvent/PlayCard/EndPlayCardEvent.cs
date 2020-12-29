using System;
using csbcgf;

namespace csbcgf
{
    public class EndPlayCardEvent : CardEvent
    {
        public EndPlayCardEvent(ICard card) : base(card)
        {
        }

        public EndPlayCardEvent(Func<ICard> getCard) : base(getCard)
        {
        }
    }
}
