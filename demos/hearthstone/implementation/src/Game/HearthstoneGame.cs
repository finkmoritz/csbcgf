using csbcgf;

namespace hearthstone
{
    public class HearthstoneGame : Game
    {
        protected HearthstoneGame()
        {
        }

        public HearthstoneGame(bool _ = true) : base(_)
        {
        }

        public void NextTurn()
        {
            ActionQueue.Execute(new NextTurnAction());
        }
    }
}
