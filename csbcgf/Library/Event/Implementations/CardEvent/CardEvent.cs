using System;

namespace csbcgf
{
    [Serializable]
    public abstract class CardEvent : Event
    {
        public ICard Card { get => GetCard(); }

        protected Func<ICard> GetCard;

        public CardEvent(ICard card) : this(() => card)
        {
        }

        public CardEvent(Func<ICard> getCard)
        {
            GetCard = getCard;
        }
    }
}
