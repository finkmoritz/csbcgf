using System;

namespace Csbcgf.Core
{
    public interface ICardComponent : IManaful, IReactive, ICloneable
    {
        /// <summary>
        /// Find the ICard this ICardComponent is attached to.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>The ICard this ICardComponent is attached to or null
        /// if it is not attached.</returns>
        ICard FindCard(IGameState gameState);
    }
}
