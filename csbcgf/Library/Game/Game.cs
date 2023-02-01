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
        public Game(bool initialize = true)
        {
            this.players = new List<IPlayer>();
            this.activePlayerIndex = 0;
            this.actionQueue = new ActionQueue(false);
            this.reactions = new List<IReaction>();

            AddReaction(new ModifyActivePlayerOnEndOfTurnEventReaction());
            AddReaction(new ModifyManaOnStartOfTurnEventReaction());
            AddReaction(new DrawCardOnStartOfTurnEventReaction());
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

        [JsonIgnore]
        public IEnumerable<ICard> CardsOnTheBoard
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (IPlayer player in Players)
                {
                    allCards.AddRange(player.Board.Cards);
                }
                return allCards.ToImmutableList();
            }
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (IPlayer player in Players)
            {
                allReactions.AddRange(player.AllReactions());
            }
            return allReactions;
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }

        public void StartGame(int initialHandSize = 4, int initialPlayerLife = 30)
        {
            //Do not trigger any reactions during setup
            actionQueue.ExecuteReactions = false;

            foreach (IPlayer player in Players)
            {
                player.ManaValue = 0;
                player.ManaBaseValue = 0;
                player.LifeValue = initialPlayerLife;
                player.LifeBaseValue = initialPlayerLife;

                for (int i = 0; i < initialHandSize; ++i)
                {
                    player.DrawCard(this);
                }
            }

            actionQueue.ExecuteReactions = true;

            Execute(new StartOfGameEvent());
            Execute(new StartOfTurnEvent());
        }

        public void NextTurn()
        {
            Execute(new EndOfTurnEvent());
            Execute(new StartOfTurnEvent());
        }

        public void Execute(IAction action)
        {
            actionQueue.Execute(this, action);
        }

        public void Execute(List<IAction> actions)
        {
            actions.ForEach(a => Execute(a));
        }

        public void AddPlayer(IPlayer player)
        {
            players.Add(player);
        }

        public bool RemovePlayer(IPlayer player)
        {
            return players.Remove(player);
        }

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }
    }
}
