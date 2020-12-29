using System;

namespace csbcgf
{
    [Serializable]
    public class EndDrawCardEvent : CardEvent
    {
        public EndDrawCardEvent(ICard card) : base(card)
        {
        }

        public EndDrawCardEvent(Func<ICard> getCard) : base(getCard)
        {
        }
    }
}
