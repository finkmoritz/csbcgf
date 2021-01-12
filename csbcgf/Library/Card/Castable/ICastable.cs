namespace csbcgf
{
    public interface ICastable
    {
        /// <summary>
        /// Checks if this Card can be cast from the Player's Hand.
        /// </summary>
        /// <returns>True if this Card can be cast from the Player's Hand.</returns>
        bool IsCastable(IGameState gameState);
    }
}
