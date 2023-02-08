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
            HearthstoneGameState state = (HearthstoneGameState)game.State;
            int newActivePlayerIndex = state.Players.ToList().IndexOf(state.ActivePlayer);
            newActivePlayerIndex = (newActivePlayerIndex + 1) % state.Players.Count();
            IPlayer newActivePlayer = state.Players.ElementAt(newActivePlayerIndex);

            game.Execute(new ModifyActivePlayerAction(newActivePlayer));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
