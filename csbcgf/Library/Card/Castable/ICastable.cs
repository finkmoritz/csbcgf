namespace csbcgf
{
    public interface ICastabledelete
    {
        /// <summary>
        /// Checks if this Card can be cast from the Player's Hand.
        /// </summary>
        /// <returns>True if this Card can be cast from the Player's Hand.</returns>
        bool IsCastabledelete(IGameState gameState);
    }
}
