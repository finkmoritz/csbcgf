using System.Collections.Immutable;
using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class HearthstoneGameState : GameState
    {
        [JsonProperty]
        protected int activePlayerIndex;
        
        protected HearthstoneGameState()
        {
        }

        public HearthstoneGameState(bool _ = true) : base(_)
        {
            this.activePlayerIndex = 0;
        }

        [JsonIgnore]
        public HearthstonePlayer ActivePlayer
        {
            get => (HearthstonePlayer)Players.ElementAt(activePlayerIndex);
            set
            {
                activePlayerIndex = Players.ToList().IndexOf(value);
            }
        }

        [JsonIgnore]
        public IEnumerable<IPlayer> NonActivePlayers
        {
            get
            {
                return Players.Where(p => p != ActivePlayer).ToImmutableList();
            }
        }
    }
}
