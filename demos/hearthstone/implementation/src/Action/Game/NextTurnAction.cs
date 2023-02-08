using csbcgf;

namespace hearthstone
{
    public class NextTurnAction : csbcgf.Action<HearthstoneGameState>
    {
        protected NextTurnAction() { }

        public NextTurnAction(bool isAborted = false)
            : base(isAborted)
        {
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            HearthstoneGameState state = game.State;

            bool wasExecuted = game.Execute(new NextPlayerAction()).Count == 1;
            if (wasExecuted)
            {
                IPlayer activePlayer = state.ActivePlayer;
                int manaDelta = activePlayer.ManaBaseValue + 1 - activePlayer.ManaValue;
                game.ExecuteSimultaneously(new List<IAction>{
                    new ModifyManaStatAction(state.ActivePlayer, manaDelta, 1),
                    new DrawCardAction(activePlayer)
                });
            }
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return true;
        }
    }
}
