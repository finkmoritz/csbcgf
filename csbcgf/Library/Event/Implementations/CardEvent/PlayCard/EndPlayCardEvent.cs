using System;

namespace csbcgf
{
    [Serializable]
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
