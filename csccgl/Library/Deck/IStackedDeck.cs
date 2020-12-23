using System;
namespace csccgl
{
    public interface IStackedDeck : IDeck
    {
        ICard PopCard();
        void PushCard(ICard card);
        void Shuffle();
    }
}
