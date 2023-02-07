using csbcgf;

namespace hearthstone
{
    public class NextPlayerAction : csbcgf.Action
    {
        protected NextPlayerAction() { }

        public NextPlayerAction(bool isAborted = false)
            : base(isAborted)
        {
        }

        public override void Execute(IGame game)
        {
            int newActivePlayerIndex = game.Players.ToList().IndexOf(game.ActivePlayer);
            newActivePlayerIndex = (newActivePlayerIndex + 1) % game.Players.Count();
            IPlayer newActivePlayer = game.Players.ElementAt(newActivePlayerIndex);

            game.ActionQueue.Execute(new ModifyActivePlayerAction(newActivePlayer));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
