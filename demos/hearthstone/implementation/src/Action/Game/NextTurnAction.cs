using csbcgf;

namespace hearthstone
{
    public class NextTurnAction : csbcgf.Action
    {
        protected NextTurnAction() { }

        public NextTurnAction(bool isAborted = false)
            : base(isAborted)
        {
        }

        public override void Execute(IGame game)
        {
            bool wasExecuted = game.ActionQueue.Execute(new NextPlayerAction()).Count == 1;
            if (wasExecuted)
            {
                IPlayer activePlayer = game.ActivePlayer;
                int manaDelta = activePlayer.ManaBaseValue + 1 - activePlayer.ManaValue;
                game.ActionQueue.ExecuteSimultaneously(new List<IAction>{
                    new ModifyManaStatAction(game.ActivePlayer, manaDelta, 1),
                    new DrawCardAction(activePlayer)
                });
            }
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
