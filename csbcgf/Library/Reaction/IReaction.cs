namespace csbcgf
{
    public interface IReaction
    {
        /// <summary>
        /// React on a given ActionEvent.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="actionEvent"></param>
        void ReactTo(IGame game, IActionEvent actionEvent);

        /// <summary>
        /// Find the player that this IReaction is attached to.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>The player that this IReaction is attached to or null
        /// if it is not attached.</returns>
        IPlayer? FindParentPlayer(IGameState gameState);
    }
}
