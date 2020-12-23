using System;
namespace csccgl
{
    [Serializable]
    public class Board : IBoard
    {
        /// <summary>
        /// Maximum number of Cards that this Board can hold.
        /// </summary>
        public const int MaxCards = 6;

        /// <summary>
        /// Data container.
        /// </summary>
        protected ICard[] Cards = new ICard[MaxCards];

        /// <summary>
        /// Represents all Cards on a Player's Board.
        /// </summary>
        public Board()
        {
        }

        public void AddAt(int index, ICard card)
        {
            if(Cards[index] != null)
            {
                throw new CsccglException("Cannot add card to board, because " +
                    "position " + index + " is already occupied!");
            }
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
