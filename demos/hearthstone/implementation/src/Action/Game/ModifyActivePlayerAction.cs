using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class ModifyActivePlayerAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstonePlayer newActivePlayer = null!;

        protected ModifyActivePlayerAction() { }

        public ModifyActivePlayerAction(HearthstonePlayer newActivePlayer, bool isAborted = false)
            : base(isAborted)
        {
            this.newActivePlayer = newActivePlayer;
        }

        [JsonIgnore]
        public HearthstonePlayer NewActivePlayer
        {
            get => newActivePlayer;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            game.State.ActivePlayer = NewActivePlayer;
        }

        public override bool IsExecutable(HearthstoneGameState state)
        {
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
