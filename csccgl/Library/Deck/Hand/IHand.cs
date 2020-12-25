using System;
namespace csccgl
{
    public interface IHand : IDeck
    {
        /// <summary>
        /// Remove the specified Card from this Hand.
        /// </summary>
        /// <param name="card"></param>
        void Remove(ICard card);

        /// <summary>
        /// Add the given Card to this Hand.
        /// </summary>
        /// <param name="card"></param>
        void Add(ICard card);
    }
}
