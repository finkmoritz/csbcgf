namespace Csbcgf.Core
{
    public abstract class Reaction : IReaction
    {
        public ICard FindParentCard(IGameState gameState)
        {
            foreach (ICard card in gameState.AllCards)
            {
                if (card.Reactions.Contains(this))
                {
                    return card;
                }
            }
            return null;
        }

        public IPlayer FindParentPlayer(IGameState gameState)
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

        public abstract object Clone();

        public abstract void ReactTo(IGame game, IActionEvent actionEvent);
    }
}
