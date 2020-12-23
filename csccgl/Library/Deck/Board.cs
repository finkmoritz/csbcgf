using System;
namespace csccgl
{
    [Serializable]
    public class Board : IBoard
    {
        public const int MaxCards = 6;
        protected ICard[] Cards = new ICard[MaxCards];

        public Board()
        {
        }

        public void AddAt(int index, ICard card)
        {
            Cards[index] = card;
        }

        public ICard RemoveAt(int index)
        {
            ICard card = Cards[index];
            Cards[index] = null;
            return card;
        }
    }
}
