using System;
namespace csccgl
{
    public interface IBoard : IDeck
    {
        /// <summary>
        /// Remove the specified Card from this Board.
        /// </summary>
        /// <param name="card"></param>
        void Remove(ICard card);

        /// <summary>
        /// Add the given Card to the Board at position index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        void AddAt(int index, ICard card);
    }
}
