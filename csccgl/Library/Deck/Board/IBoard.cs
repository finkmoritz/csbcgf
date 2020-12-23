using System;
namespace csccgl
{
    public interface IBoard : IDeck
    {
        /// <summary>
        /// Remove the Card at position index and return that Card.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The removed Card or null if there was no
        /// Card at position index.</returns>
        ICard RemoveAt(int index);

        /// <summary>
        /// Add the given Card to the Board at position index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        void AddAt(int index, ICard card);
    }
}
