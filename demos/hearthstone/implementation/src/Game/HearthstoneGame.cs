using csbcgf;

namespace hearthstone
{
    public class HearthstoneGame : Game
    {
        protected HearthstoneGame()
        {
        }

        public HearthstoneGame(HearthstoneGameState gameState) : base(gameState)
        {
        }

        public void NextTurn()
        {
            Execute(new NextTurnAction());
        }
    }
}
