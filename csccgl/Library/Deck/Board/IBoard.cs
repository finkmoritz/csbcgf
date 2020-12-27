using System;
namespace csbcgf
{
    public interface IBoard : IDeck
    {
        /// <summary>
        /// Maximum number of Cards that this Board can hold.
        /// </summary>
        int MaxSize { get; }

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

        /// <summary>
        /// Get the Card at position index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The Card at position index.</returns>
        ICard Get(int index);

        /// <summary>
        /// Checks if the given position points to a free slot on this Board.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True if the given position points to a free slot on
        /// this Board.</returns>
        bool IsFreeSlot(int index);
    }
}
