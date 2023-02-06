using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class Game : IGame
    {
        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        [JsonProperty]
        protected int activePlayerIndex;

        [JsonProperty]
        protected ActionQueue actionQueue = null!;

        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected List<IPlayer> players = null!;

        protected Game()
        {
        }

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        public Game(bool _ = true)
        {
            this.players = new List<IPlayer>();
            this.activePlayerIndex = 0;
            this.actionQueue = new ActionQueue(this);
            this.reactions = new List<IReaction>();

            AddReaction(new ModifyActivePlayerOnEndOfTurnEventReaction());
            AddReaction(new ModifyManaOnStartOfTurnEventReaction());
            AddReaction(new DrawCardOnStartOfTurnEventReaction());
        }

        [JsonIgnore]
        public IActionQueue ActionQueue
        {
            get => actionQueue;
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

        public void Start()
        {
            ActionQueue.ExecuteSequentially(new List<IAction> {
                new StartOfGameEvent(),
                new StartOfTurnEvent()
            });
        }

        public void NextTurn()
        {
            ActionQueue.ExecuteSequentially(new List<IAction> {
                new EndOfTurnEvent(),
                new StartOfTurnEvent()
            });
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
