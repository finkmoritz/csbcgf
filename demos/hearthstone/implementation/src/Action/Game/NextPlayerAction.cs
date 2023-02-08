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
            int newActivePlayerIndex = game.State.Players.ToList().IndexOf(game.State.ActivePlayer);
            newActivePlayerIndex = (newActivePlayerIndex + 1) % game.State.Players.Count();
            IPlayer newActivePlayer = game.State.Players.ElementAt(newActivePlayerIndex);

            game.Execute(new ModifyActivePlayerAction(newActivePlayer));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
