using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class Game : IGame
    {
        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        [JsonProperty]
        protected int activePlayerIndex;

        [JsonProperty]
        protected ActionQueue actionQueue;

        public IPlayer[] Players { get; protected set; }

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        public Game(IPlayer[] players)
            : this(players, new Random().Next(players.Length), new ActionQueue(false))
        {
        }

        [JsonConstructor]
        public Game(IPlayer[] players, int activePlayerIndex, ActionQueue actionQueue)
        {
            Players = players;
            this.activePlayerIndex = activePlayerIndex;
            this.actionQueue = actionQueue;
        }

        [JsonIgnore]
        public IPlayer ActivePlayer
        {
            get => Players[activePlayerIndex];
            set
            {
                for (int i = 0; i < Players.Length; ++i)
                {
                    if (Players[i] == value)
                    {
                        activePlayerIndex = i;
                    }
                }
            }
        }

        [JsonIgnore]
        public List<IPlayer> NonActivePlayers
        {
            get
            {
                return Players.Where(p => p != ActivePlayer).ToList();
            }
        }

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (IPlayer player in Players)
                {
                    allCards.AddRange(player.AllCards);
                }
                return allCards;
            }
        }

        [JsonIgnore]
        public List<ICard> AllCardsOnTheBoard
        {
            get
            {
                List<ICard> allCards = new List<ICard>();
                foreach (IPlayer player in Players)
                {
                    allCards.AddRange(player.Board.AllCards);
                }
                return allCards;
            }
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
            NextTurn();
        }

        public void NextTurn()
        {
            Execute(new ModifyActivePlayerAction(Players[(activePlayerIndex+1) % Players.Length]));

            int manaDelta = ActivePlayer.ManaBaseValue + 1 - ActivePlayer.ManaValue;
            Execute(new ModifyManaStatAction(ActivePlayer, manaDelta, 1));

            ActivePlayer.DrawCard(this);

            Execute(new StartOfTurnEvent());
        }

        public void Execute(IAction action)
        {
            actionQueue.Enqueue(action);
            actionQueue.Process(this);
        }

        public void Execute(List<IAction> actions)
        {
            actionQueue.Enqueue(actions);
            actionQueue.Process(this);
        }
    }
}
