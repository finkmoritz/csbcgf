using System;
namespace csccgl
{
    public interface IHand : IDeck
    {
        /// <summary>
        /// Remove the Card at position index from this Hand.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The removed Card</returns>
        ICard RemoveAt(int index);

        /// <summary>
        /// Add the given Card to this Hand.
        /// </summary>
        /// <param name="card"></param>
        void Add(ICard card);
    }
}
