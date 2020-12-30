using System;
using System.Collections.Generic;

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

        protected ActionQueue actionQueue = new ActionQueue(false);

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="initialHandSize"></param>
        public Game(IPlayer[] players, int initialPlayerLife = 30, int initialHandSize = 4)
        {
            if(players.Length != 2)
            {
                throw new CsbcgfException("Parameter 'players' must feature exactly two Player entries!");
            }

            Players = players;

            Init(initialPlayerLife, initialHandSize);
        }

        protected void Init(int initialPlayerLife, int initialHandSize)
        {
            //Do not trigger any reactions during setup
            actionQueue.ExecuteReactions = false;

            foreach (IPlayer player in Players)
            {
                player.ManaValue = 0;
                player.ManaBaseValue = 0;
                player.LifeValue = initialPlayerLife;
                player.LifeBaseValue = initialPlayerLife;

                player.AllCards.ForEach(c => c.Owner = player);

                for (int i = 0; i < initialHandSize; ++i)
                {
                    player.DrawCard(this);
                }
            }

            actionQueue.ExecuteReactions = true;

            StartGame();
        }

        protected void StartGame()
        {
            Execute(new StartOfGameEvent());
            ActivePlayerIndex = new Random().Next(Players.Length);
            NextTurn();
        }

        public void NextTurn()
        {
            ActivePlayerIndex = (ActivePlayerIndex + 1) % Players.Length;

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
