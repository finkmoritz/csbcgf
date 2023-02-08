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
            int newActivePlayerIndex = game.GameState.Players.ToList().IndexOf(game.GameState.ActivePlayer);
            newActivePlayerIndex = (newActivePlayerIndex + 1) % game.GameState.Players.Count();
            IPlayer newActivePlayer = game.GameState.Players.ElementAt(newActivePlayerIndex);

            game.Execute(new ModifyActivePlayerAction(newActivePlayer));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
