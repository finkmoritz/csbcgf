using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class ModifyActivePlayerAction : csbcgf.Action
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
            HearthstoneGameState state = (HearthstoneGameState)game.State;
            state.ActivePlayer = NewActivePlayer;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            HearthstoneGameState state = (HearthstoneGameState)gameState;
            if (!state.Players.Contains(NewActivePlayer))
            {
                throw new CsbcgfException("Could not change the active " +
                    "player because the specified player is not involved " +
                    "in the game!");
            }
            return NewActivePlayer != state.ActivePlayer;
        }
    }
}
