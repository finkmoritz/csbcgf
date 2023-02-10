using csbcgf;

namespace hearthstone
{
    public class NextPlayerAction : csbcgf.Action<HearthstoneGameState>
    {
        protected NextPlayerAction() { }

        public NextPlayerAction(bool isAborted = false)
            : base(isAborted)
        {
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            HearthstoneGameState state = game.State;
            int newActivePlayerIndex = state.Players.ToList().IndexOf(state.ActivePlayer);
            newActivePlayerIndex = (newActivePlayerIndex + 1) % state.Players.Count();
            HearthstonePlayer newActivePlayer = (HearthstonePlayer)state.Players.ElementAt(newActivePlayerIndex);

            game.Execute(new ModifyActivePlayerAction(newActivePlayer));
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return true;
        }
    }
}
