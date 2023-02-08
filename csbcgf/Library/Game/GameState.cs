using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class GameState : IGameState
    {
        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        [JsonProperty]
        protected int activePlayerIndex;

        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected List<IPlayer> players = null!;

        protected GameState()
        {
        }

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        public GameState(bool _ = true)
        {
            this.players = new List<IPlayer>();
            this.activePlayerIndex = 0;
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        [JsonIgnore]
        public IEnumerable<IPlayer> Players
        {
            get => players.ToImmutableList();
        }

        [JsonIgnore]
        public IPlayer ActivePlayer
        {
            get => Players.ElementAt(activePlayerIndex);
            set
            {
                activePlayerIndex = players.IndexOf(value);
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

        [JsonIgnore]
        public IEnumerable<ICard> Cards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (IPlayer player in Players)
                {
                    allCards.AddRange(player.AllCards);
                }
                return allCards.ToImmutableList();
            }
        }

        public IEnumerable<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (IPlayer player in Players)
            {
                allReactions.AddRange(player.AllReactions());
            }
            return allReactions.ToImmutableList();
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public bool RemovePlayer(IPlayer player)
        {
            return players.Remove(player);
        }
    }
}
