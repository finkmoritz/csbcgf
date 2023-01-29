namespace csbcgf
{
    public abstract class Reaction : IReaction
    {
        public IPlayer? FindParentPlayer(IGameState gameState)
        {
            foreach (IPlayer player in gameState.Players)
            {
                if (player.Reactions.Contains(this))
                {
                    return player;
                }
            }
            return null;
        }

        public abstract void ReactTo(IGame game, IActionEvent actionEvent);
    }
}
