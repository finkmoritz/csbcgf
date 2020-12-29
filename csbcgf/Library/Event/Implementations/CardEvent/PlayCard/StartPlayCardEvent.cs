using System;

namespace csbcgf
{
    [Serializable]
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
