using System;
namespace csbcgf
{
    public interface ICompoundCard : ICard
    {
        /// <summary>
        /// Add a Card to this CompoundCard.
        /// </summary>
        /// <param name="card"></param>
        void AddComponent(ICard card);

        /// <summary>
        /// Remove a Card from this CompoundCard.
        /// </summary>
        /// <param name="card"></param>
        void RemoveComponent(ICard card);
    }
}
