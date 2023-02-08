using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyActivePlayerAction : Action
    {
        [JsonProperty]
        protected IPlayer newActivePlayer = null!;

        protected ModifyActivePlayerAction() { }

        public ModifyActivePlayerAction(IPlayer newActivePlayer, bool isAborted = false)
            : base(isAborted)
        {
            this.newActivePlayer = newActivePlayer;
        }

        [JsonIgnore]
        public IPlayer NewActivePlayer
        {
            get => newActivePlayer;
        }

        public override void Execute(IGame game)
        {
            game.State.ActivePlayer = NewActivePlayer;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            if (!gameState.Players.Contains(NewActivePlayer))
            {
                throw new CsbcgfException("Could not change the active " +
                    "player because the specified player is not involved " +
                    "in the game!");
            }
            return NewActivePlayer != gameState.ActivePlayer;
        }
    }
}
