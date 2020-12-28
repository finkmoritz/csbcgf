using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class Game : IGame
    {
        public IPlayer[] Players { get; }

        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        public int ActivePlayerIndex { get; protected set; }

        public IPlayer ActivePlayer => Players[ActivePlayerIndex];

        public IPlayer NonActivePlayer => Players[1 - ActivePlayerIndex];

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

        /// <summary>
        /// Additional GameOptions that help customizing a Game.
        /// </summary>
        public readonly GameOptions Options;

        protected ActionQueue actions = new ActionQueue();

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="options"></param>
        public Game(IPlayer[] players, GameOptions options = null)
        {
            if(players.Length != 2)
            {
                throw new CsbcgfException("Parameter 'players' must feature exactly two Player entries!");
            }

            Players = players;
            ActivePlayerIndex = new Random().Next(Players.Length);
            Options = options ?? new GameOptions();

            Init(Options);

            actions.Process(this);
        }

        protected void Init(GameOptions options)
        {
            foreach (IPlayer player in Players)
            {
                player.ManaValue = 0;
                player.ManaBaseValue = 0;
                player.LifeValue = options.InitialPlayerLife;

                player.AllCards.ForEach(c => c.Owner = player);

                for (int i = 0; i < Options.InitialHandSize; ++i)
                {
                    player.DrawCard(this);
                }
            }

            NextTurn();
        }

        public void NextTurn()
        {
            ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;

            int manaDelta = ActivePlayer.ManaBaseValue + 1 - ActivePlayer.ManaValue;
            Queue(new ModifyManaStatAction(ActivePlayer, manaDelta, 1));

            ActivePlayer.DrawCard(this);
            Queue(new StartOfTurnEvent());

            actions.Process(this);
        }

        public void Queue(IAction action)
        {
            actions.Queue(action);
        }

        public void Process()
        {
            actions.Process(this);
        }
    }
}
