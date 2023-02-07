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
            AddReaction(new ModifyActivePlayerOnEndOfTurnEventReaction());
            AddReaction(new ModifyManaOnStartOfTurnEventReaction()); //TODO move to player
            AddReaction(new DrawCardOnStartOfTurnEventReaction()); //TODO move to player
        }

        public void Start()
        {
            ActionQueue.ExecuteSequentially(new List<IAction> {
                new StartOfGameEvent(),
                new StartOfTurnEvent()
            });
        }

        public void NextTurn()
        {
            ActionQueue.ExecuteSequentially(new List<IAction> {
                new EndOfTurnEvent(),
                new StartOfTurnEvent()
            });
        }
    }
}
