namespace csbcgf
{
    public interface ICardComponent : IManaful, IReactive
    {
        /// <summary>
        /// Get the ICard this ICardComponent is attached to.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>The ICard this ICardComponent is attached to.</returns>
        ICard Card { get; }
    }
}
