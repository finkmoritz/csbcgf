using System;
using System.Collections.Generic;
using System.Linq;

namespace csbcgf
{
    public class Game : IGame
    {
        /// <summary>
        /// Index of the active Player. Refers to the Players array.
        /// Also see the ActivePlayer accessor.
        /// </summary>
        protected int activePlayerIndex;

        protected ActionQueue actionQueue;

        public List<IReaction> Reactions { get; }

        public List<IPlayer> Players { get; protected set; }

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        public Game() : this(new List<IPlayer>())
        {
        }

        /// <summary>
        /// Represent the current Game state and provides methods to alter
        /// this Game state.
        /// </summary>
        /// <param name="players"></param>
        public Game(List<IPlayer> players)
            : this(players, new Random().Next(players.Count), new ActionQueue(false), new List<IReaction>())
        {
            Reactions.Add(new ModifyActivePlayerOnEndOfTurnEventReaction());
            Reactions.Add(new ModifyManaOnStartOfTurnEventReaction());
            Reactions.Add(new DrawCardOnStartOfTurnEventReaction());
        }

        public Game(List<IPlayer> players, int activePlayerIndex, ActionQueue actionQueue, List<IReaction> reactions)
        {
            Players = players;
            this.activePlayerIndex = activePlayerIndex;
            this.actionQueue = actionQueue;
            Reactions = reactions;
        }

        public IPlayer ActivePlayer
        {
            get => Players[activePlayerIndex];
            set
            {
                activePlayerIndex = Players.IndexOf(value);
            }
        }

        public List<IPlayer> NonActivePlayers
        {
            get
            {
                return Players.Where(p => p != ActivePlayer).ToList();
            }
        }

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

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            Players.ForEach(p => allReactions.AddRange(p.AllReactions()));
            return allReactions;
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

        public void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public object Clone()
        {
            List<IPlayer> playersClone = new List<IPlayer>();
            foreach (IPlayer player in Players)
            {
                playersClone.Add((IPlayer)player.Clone());
            }

            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new Game(
                playersClone,
                activePlayerIndex,
                (ActionQueue)actionQueue.Clone(),
                reactionsClone
            );
        }

        public ICard FindParentCard(IGameState gameState)
        {
            throw new CsbcgfException("Cannot use method 'FindParentCard' on " +
                "instance of type 'Game'");
        }

        public IPlayer FindParentPlayer(IGameState gameState)
        {
            throw new CsbcgfException("Cannot use method 'FindParentPlayer' on " +
                "instance of type 'Game'");
        }
    }
}
